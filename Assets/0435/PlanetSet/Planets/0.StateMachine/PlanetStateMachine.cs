using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetStateMachine : MonoBehaviour
{
    private float deleteDistance = 40;

    //ステートマシンの実装
    public enum State
    {
        Idle,          //通常状態
        Catch,         //キャッチ状態
        Throw,         //投てき状態
        Hit,           //ヒット時

        GETNUM_NOT_USE,//状態の数を取得する。この状態を使用するのは禁止

        Non,           //無効状態
        Destroy,       //破壊モード

        Now,           //関数の戻り値として使用。現在の状態を維持させる
        Error,         //エラーを吐き出す状態。この値に変更するとエラーを吐く
    }
    [Header("初期化に設定する状態")]
    public State firstState;

    [Header("現在の状態（直接変更はデバッグ時のみにする。変更時はChangeState関数で）")]
    public State nowState;

    [System.Serializable]
    public class ScriptData
    {
        [SerializeField]
        PlanetStateFanction[] scripts = new PlanetStateFanction[1];
        public ref PlanetStateFanction GetScript(int num) { return ref scripts[num]; }
        public ref PlanetStateFanction[] GetScripts() { return ref scripts; }
        public int GetScriptLength() { return scripts.Length; }
    }

    [Header("状態ごとに使用するスクリプト。状態変更は下のほうが優先度が高い")]
    [SerializeField]
    public ScriptData[] data = new ScriptData[(int)State.GETNUM_NOT_USE];

    // Start is called before the first frame update
    void Start()
    {
        nowState = firstState;
    }

    // Update is called once per frame
    void Update()
    {
        float tdt = Time.deltaTime;//deltaTime

        //スクリプト実行(次の状態も同時に決定)
        State nextState = UpdateStateFanction(tdt);

        //状態変化
        ChangeState(nextState);
        //距離で削除
        PlantDestroyByPos();

        //破壊状態なら破壊
        if (nowState == State.Destroy)
        {
            Destroy(gameObject);
        }
    }

    private State UpdateStateFanction(float tdt)
    {
        //状態を現在に変更する
        State Next = State.Now;

        //スクリプトを実行する
        for (int i = 0; i < data[(int)nowState].GetScriptLength(); ++i)
        {
            Next = data[(int)nowState].GetScript(i).Fanction(tdt);
        }
        return Next;
    }

    private void ChangeState(State state)
    {
        if (state != State.Error)//エラーではない場合（Now以外の場合）
        {
            if (state != State.Now)//現在の状態を維持しない場合（Now以外の場合）
            {
                //状態変更
                nowState = state;
            }
        }
        else
        {
            Debug.Log("エラー：状態(State)がErrorに変更されました。");
        }
    }

    public void SetState(State state)
    {
        if (state != State.Error)//エラーではない場合（Now以外の場合）
        {
            if (state != State.Now)//現在の状態を維持しない場合（Now以外の場合）
            {
                //状態変更
                nowState = state;
            }
        }
        else
        {
            Debug.Log("エラー：状態(State)がErrorに変更されました。");
        }
    }

    public PlanetStateMachine.State GetState()
    {
        return nowState;
    }

    private void PlantDestroyByPos()
    {
        Vector3 pos = this.transform.position;

        if (pos.x >= deleteDistance ||
            pos.x <= -deleteDistance ||
            pos.z >= deleteDistance ||
            pos.z <= -deleteDistance)
        {
            SetState(State.Destroy);
        }
    }
}
