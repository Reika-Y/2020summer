using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

// タッチ判定用
public class TouchEventHandler : SingletonMonoBehaviour<TouchEventHandler>,IPointerDownHandler,IPointerUpHandler,IBeginDragHandler,IEndDragHandler,IDragHandler
{
    #region <変数>

    // タップ中
    private bool isPressing = false;
    public bool IsPressing
    {
        get { return isPressing; }
    }

    // ドラック中か
    private bool isDragging = false;
    public bool IsDragging
    {
        get { return isDragging; }
    }

    // ピンチ中か
    private bool isPinching = false;
    public bool IsPinching
    {
        get { return isPinching; }
    }

    // ドラック位置
    private Vector3 beforeTapWorldPoint;

    // ピンチ開始時の指の距離
    private float beforeDistanceOfPinch;

    // タップ関係
    public event Action<bool> onPress = delegate { };
    public event Action onBeginPress = delegate { };
    public event Action onEndPress = delegate { };

    // ドラッグ
    public event Action<Vector2> onDrag = delegate { };
    public event Action<Vector3> onDragIn3D = delegate { };
    public event Action onBeginDrag = delegate { };
    public event Action onEndDrag = delegate { };

    // ピンチ
    public event Action<float> onPitch = delegate { };
    public event Action onBeginPitch = delegate { };
    public event Action onEndPitch = delegate { };

    // ドラッグしている指のデータ
    private Dictionary<int, PointerEventData> draggingDataDict = new Dictionary<int, PointerEventData>();

    #endregion

    #region <ドラッグ>

    // ドラッグ開始時
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(!isDragging)
        {
            beforeTapWorldPoint = GetTapWorldPoint();
        }
        isDragging = true;
        onBeginDrag();

        // ドラッグデータ追加
        draggingDataDict[eventData.pointerId] = eventData;
    }

    // ドラッグ中
    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging)
        {
            OnBeginDrag(eventData);
            return;
        }

        // ドラッグデータ更新
        draggingDataDict[eventData.pointerId] = eventData;

        // 2本以上ドラッグがある時はピンチ
        if(draggingDataDict.Count >= 2)
        {
            // ドラッグ中なら終了
            if(isDragging)
            {
                isDragging = false;
                onEndDrag();
            }
            OnPitch();
        }
        else if(Input.touches.Length <= 1)
        {
            if(isPinching)
            {
                isDragging = false;
                isPinching = false;
                onEndPitch();
                OnBeginDrag(eventData);
            }
            onDrag(eventData.delta);
            OnDragInWorldPoint();
        }
    }

    // ドラッグ終了時
    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        onEndDrag();

        // ドラッグデータ削除
        if (draggingDataDict.ContainsKey(eventData.pointerId))
        {
            draggingDataDict.Remove(eventData.pointerId);
        }
    }

    public void OnDragInWorldPoint()
    {
        Vector3 tapWorldPoint = GetTapWorldPoint();
        onDragIn3D(tapWorldPoint - beforeTapWorldPoint);
        beforeTapWorldPoint = tapWorldPoint;
    }

    //タップしている場所をワールド座標で取得
    private Vector3 GetTapWorldPoint()
    {
        //タップ位置を画面内の座標に変換
        Vector2 screenPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(),
          new Vector2(Input.mousePosition.x, Input.mousePosition.y),
          null,
          out screenPos
        );

        //ワールド座標に変換
        Vector3 tapWorldPoint = Camera.main.ScreenToWorldPoint( new Vector3(screenPos.x, screenPos.y, -Camera.main.transform.position.z));

        return tapWorldPoint;
    }

    #endregion

    #region <タップ>

    // タップ開始時
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressing = true;
        onBeginPress();
        onPress(true);
    }

    // タップ終了時
    public void OnPointerUp(PointerEventData eventData)
    {
        isPressing = false;
        onEndPress();
        onPress(false);

        // ピンチ終了イベント実行
        if(isPinching && draggingDataDict.Count < 2)
        {
            isPinching = false;
            onEndPitch();
        }
    }

    #endregion

    #region <ピンチ>

    // ピンチ中
    private void OnPitch()
    {
        // 最初と２本目のタッチ情報取得
        Vector2 firstTouch = Vector2.zero, secondTouch = Vector2.zero;
        int count = 0;

        foreach (var draggingData in draggingDataDict)
        {
            if (count == 0)
            {
                firstTouch = draggingData.Value.position;
                count = 1;
            }
            else if (count == 1)
            {
                secondTouch = draggingData.Value.position;
                break;
            }
        }

        // 幅の取得
        float distanceOfPinch = Vector2.Distance(firstTouch, secondTouch);

        // 開始
        if(isPinching)
        {
            isPinching = true;
            beforeDistanceOfPinch = distanceOfPinch;

            onBeginPitch();
            return;
        }

        // 倍率計算
        float pinchDiff = distanceOfPinch - beforeDistanceOfPinch;
        onPitch(pinchDiff);

        beforeDistanceOfPinch = distanceOfPinch;
    }
    
    #endregion
}
