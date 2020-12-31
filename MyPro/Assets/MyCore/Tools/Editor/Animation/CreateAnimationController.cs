using UnityEditor;
using UnityEngine;
using System.Collections;
using CSF;
using System.IO;
using UnityEditor.Animations;
using System.Collections.Generic;

public class CreateAnimationController : EditorWindow
{
    const string AnimatorDir = @"Assets/GameRes/ArtRes/Animator/";
    [MenuItem("Assets/Create/创建AnimationController", priority = 0)]
    static void CreateController()
    {
        List<string> animatorNames = GetAnimatorFolder();
        foreach(string animatorName in animatorNames)
            CreateAnimatorController(AnimatorDir+ animatorName+"/", animatorName);
        AssetDatabase.Refresh();       
    }
    [MenuItem("Assets/Create/创建AnimationController", true,priority = 0)]
    static bool CheckCreateController()
    {       
        return GetAnimatorFolder().Count!=0;
    }

    static List<string> GetAnimatorFolder()
    {
        string[] guidArray = Selection.assetGUIDs;
        List<string> rtnAnimatorNames = new List<string>();
        foreach (var item in guidArray)
        {
            string selectFloder = AssetDatabase.GUIDToAssetPath(item);
            string name = null;
            if (selectFloder.StartsWith(AnimatorDir))
            {
                name = selectFloder.Remove(0, AnimatorDir.Length);
                if (name.IndexOf('.') == -1 && name.IndexOf('/') == -1)
                    rtnAnimatorNames.Add(name);
            }
        }
        return rtnAnimatorNames;
    }

    private static void CreateAnimatorController(string animatorPath,string animatorName)
    {
        List<string> animActions = GetActionNames(animatorPath);
       

        string controllerPath = animatorPath + animatorName + ".controller";
        AnimatorController _animatorController  = AssetDatabase.LoadAssetAtPath<AnimatorController>(controllerPath);

        if (_animatorController == null)
        {
            //生成动画控制器（AnimatorController）
            _animatorController = AnimatorController.CreateAnimatorControllerAtPath(controllerPath);
        }
        else
        {
            for (int i = _animatorController.parameters.Length; --i >= 0;)
                _animatorController.RemoveParameter(i);

            if (_animatorController.layers.Length == 0)
            {
                _animatorController.AddLayer("Base Layer");
            }
            AnimatorStateMachine _machine = _animatorController.layers[0].stateMachine;
            for (int i = _machine.states.Length; --i >= 0;)
                _machine.RemoveState(_machine.states[i].state);
        }

        //添加参数（parameters）
        _animatorController.AddParameter(State, AnimatorControllerParameterType.Int);  //0待机 1移动  //循环状态
        _animatorController.AddParameter(Hit, AnimatorControllerParameterType.Trigger);
        foreach (string anim in animActions)
            _animatorController.AddParameter(anim, AnimatorControllerParameterType.Trigger);

        //得到它的Layer， 默认layer为base,可以拓展
        AnimatorControllerLayer _layer = _animatorController.layers[0];
        AddStateActionTransition(_layer, animatorPath);
        AddActionTransition(_layer,animatorPath, animActions);

    }
    static string[] animStates = new string[] { "Idle", "Move" };
    static Dictionary<string, string> animDefault = new Dictionary<string, string>() { { "Idle", "Move" }, { "Move", "Idle" } };
    //static string[] animActions = new string[] {"Die", "Attack1", "Attack2", "Attack3", "AttackEnd1", "AttackEnd2", "AttackEnd3", "Motion1" , "Motion2" };
    static string Idle = "Idle";
    static string Move = "Move";
    static string Hit = "Hit";
    static string State = "State";
    static string Die = "Die";



    private static void AddStateActionTransition(AnimatorControllerLayer _layer,string animatorPath)
    {
        AnimatorStateMachine _machine = _layer.stateMachine;
        _machine.entryPosition = new Vector3(_machine.anyStatePosition.x, _machine .anyStatePosition.y- 100, 0);
        _machine.exitPosition = new Vector3(_machine.anyStatePosition.x + 650, _machine.anyStatePosition.y, 0);
               
        AnimatorState _idleState = _machine.AddState(Idle, new Vector3(_machine.entryPosition.x + 170, _machine.entryPosition.y-100, 0));
        _idleState.motion = GetAnimationClip(animatorPath,Idle);

        AnimatorState _moveState = _machine.AddState(Move, new Vector3(_machine.entryPosition.x + 430, _machine.entryPosition.y-100, 0));
        _moveState.motion = GetAnimationClip(animatorPath,Move);

        //Idle<--->Move
        AnimatorStateTransition _trans;
        _trans = _idleState.AddTransition(_moveState);
        _trans.AddCondition(AnimatorConditionMode.Equals, 1, State);
        _trans.hasExitTime = false;
        _trans = _moveState.AddTransition(_idleState);
        _trans.AddCondition(AnimatorConditionMode.Equals, 0, State);
        _trans.hasExitTime = false;

        var hitMotion = GetAnimationClip(animatorPath, Hit);
        if (hitMotion != null)
        {
            AnimatorState _hitState = _machine.AddState(Hit, new Vector3(_machine.entryPosition.x + 300, _machine.entryPosition.y, 0));
            _hitState.motion = hitMotion;
            //Idle<--->Hit
            TransitionHit(_idleState, _hitState, 0);
            //Move<--->Hit
            TransitionHit(_moveState, _hitState, 1);
        }
    }

    //Hit状态双向连接
    private static void TransitionHit(AnimatorState state, AnimatorState stateHit,int stateVal)
    {
        AnimatorStateTransition _trans = state.AddTransition(stateHit);
        _trans.AddCondition(AnimatorConditionMode.Equals, stateVal, State);
        _trans.AddCondition(AnimatorConditionMode.If, 0, Hit);
        _trans.hasExitTime = false;

        AnimatorStateTransition _transExit = stateHit.AddTransition(state);
        _transExit.AddCondition(AnimatorConditionMode.Equals, stateVal, State);
        _transExit.hasExitTime = false;

        if (stateHit.motion == null)
        {
            _trans.duration = 0;
            _transExit.duration = 0;
            _transExit.exitTime = 0f;
        }
        else
        {
            _trans.duration = 0.12f;
            _transExit.duration = 0.25f;
            _transExit.exitTime = 0.75f;
        }
    }


    private static void AddActionTransition(AnimatorControllerLayer _layer, string animatorPath, List<string> animActions)
    {       
        AnimatorStateMachine _machine = _layer.stateMachine;
        AnimatorState _state;      
        string stateName;   
        //设置单次动作============
        for (int i = 0; i < animActions.Count; i++)
        {
            stateName = animActions[i];
            _state = _machine.AddState(stateName, new Vector3(_machine.anyStatePosition.x + 300, _machine.anyStatePosition.y + i * 80, 0));
            //连接每个状态，并添加切换条件
            AnimatorStateTransition _animatorStateTransition = _machine.AddAnyStateTransition(_state);
            _animatorStateTransition.AddCondition(AnimatorConditionMode.If, 0, stateName);
            _animatorStateTransition.hasExitTime = false;
            //_animatorStateTransition.duration = 0;


            AnimationClip clip = GetAnimationClip(animatorPath, stateName);
            AnimatorStateTransition _animatorStateExit = null;
            if (!clip.isLooping)
            {
                _animatorStateExit = _state.AddExitTransition();
                _animatorStateExit.hasExitTime = true;
                
            }

            _state.motion = clip;
            if (stateName == Die)
            {
                _animatorStateTransition.duration = 0.1f;
                if(_animatorStateExit!=null)
                    _animatorStateExit.exitTime = 1f;
            }
            else
            {                
                _animatorStateTransition.duration = 0.1f;
                if (_animatorStateExit != null)
                    _animatorStateExit.exitTime = 0.9f;

            }


            //if (clip == null)
            //{
            //    _animatorStateTransition.duration = 0;
            //    _animatorStateExit.exitTime = 0f;
            //}
            //else
            //{
            //    _state.motion = clip;
            //    if (stateName == Die)
            //    {
            //        _animatorStateExit.exitTime = 1f;
            //        _animatorStateTransition.duration = 0.1f;
            //    }
            //    else
            //    {
            //        _animatorStateExit.exitTime = 0.9f;
            //        _animatorStateTransition.duration = 0.1f;
            //    }
            //}
        }
    }

    private static AnimationClip GetAnimationClip(string animatorPath, string name)
    {
        AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(animatorPath + name + ".anim");
        if (clip == null)
        {
            if(animDefault.TryGetValue(name,out var defName)) //找不到使用默认代码
                clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(animatorPath + defName + ".anim");
        }            
        return clip;
    }

    private static List<string> GetActionNames(string animatorPath)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(animatorPath);
        FileInfo[] fileInfos = directoryInfo.GetFiles("*.anim", SearchOption.TopDirectoryOnly);
        List<string> actionNames = new List<string>();
        string animName;
        for (int i = 0; i < fileInfos.Length; i++)
        {
            animName = Path.GetFileNameWithoutExtension(fileInfos[i].FullName);
            if (animName!= Idle && animName!=Hit && animName!=Move)
                actionNames.Add(animName);
        }
        return actionNames;
    }
}