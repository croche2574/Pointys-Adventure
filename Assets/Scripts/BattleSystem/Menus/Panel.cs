using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(LayoutAnchor))]
public class Panel : MonoBehaviour
{
    [Serializable]
    public class Position
    {
        public string name;
        public TextAnchor thisAnchor;
        public TextAnchor parentAnchor;
        public Vector2 offset;
        public Position(string name)
        {
            this.name = name;
        }
        public Position(string name, TextAnchor thisAnchor, TextAnchor parentAnchor) : this(name)
        {
            this.thisAnchor = thisAnchor;
            this.parentAnchor = parentAnchor;
        }
        public Position(string name, TextAnchor thisAnchor, TextAnchor parentAnchor, Vector2 offset) : this(name, thisAnchor, parentAnchor)
        {
            this.offset = offset;
        }
    }

    [SerializeField] List<Position> positionList;
    Dictionary<String, Position> positionMap;
    LayoutAnchor anchor;

    void Awake()
    {
        anchor = GetComponent<LayoutAnchor>();
        positionMap = new Dictionary<string, Position>(positionList.Count);
        for (int i = positionList.Count-1; i >= 0; --i)
            AddPosition(positionList[i]);
    }

    public Position CurrentPosition{get; private set;}
    public Tweener Transition{get; private set;}
    public bool InTransition{get{return Transition != null;}}
    public Position this[string name]
    {
        get
        {
            if (positionMap.ContainsKey(name))
                return positionMap[name];
            return null;
        }
    }
    public void AddPosition(Position p)
    {
        positionMap[p.name] = p;
    }
    public void RemovePosition(Position p)
    {
        if (positionMap.ContainsKey(p.name))
            positionMap.Remove(p.name);
    }
    public Tweener SetPosition(string posName, bool animated)
    {
        return SetPosition(this[posName], animated);
    }
    public Tweener SetPosition(Position p, bool animated)
    {
        CurrentPosition = p;
        if (CurrentPosition == null)
        {
            return null;
        }
        if (InTransition)
        {
            Transition.easingControl.Stop();
        }
        if (animated)
        {
            Transition = anchor.MoveToAnchorPosition(p.thisAnchor, p.parentAnchor, p.offset);
            return Transition;
        }
        else
        {
            anchor.SnapToAnchorPosition(p.thisAnchor, p.parentAnchor, p.offset);
            return null;
        }
    }
    void Start()
        {
            if (CurrentPosition == null && positionList.Count > 0)
                SetPosition(positionList[0], false);
        }
}
