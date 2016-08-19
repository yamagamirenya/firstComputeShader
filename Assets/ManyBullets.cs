﻿using UnityEngine;
using System.Runtime.InteropServices;
 
/// <summary>
/// 弾の構造体
/// </summary>
struct Bullet
{
    /// <summary>
    /// 座標
    /// </summary>
    public Vector3 pos;

    /// <summary>
    /// 速度
    /// </summary>
    public Vector3 accel;

    /// <summary>
    /// 色
    /// </summary>
    public Color color;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public Bullet(Vector3 pos, Vector3 accel, Color color)
    {
        this.pos = pos;
        this.accel = accel;
        this.color = color;
    }
}

/// <summary>
/// 沢山の弾を管理するクラス
/// </summary>
public class ManyBullets: MonoBehaviour
{

    public GameObject mainCamera;

    /// <summary>
    /// 弾をレンダリングするシェーダー
    /// </summary>
    public Shader bulletsShader;

    /// <summary>
    /// 弾のテクスチャ
    /// </summary>
    public Texture bulletsTexture;

    /// <summary>
    /// 弾の更新を行うコンピュートシェーダー
    /// </summary>
    public ComputeShader bulletsComputeShader;

    /// <summary>
    /// 弾のマテリアル
    /// </summary>
    Material bulletsMaterial;

    /// <summary>
    /// 弾のコンピュートバッファ
    /// </summary>
    ComputeBuffer bulletsBuffer;


    /// <summary>
    /// 破棄
    /// </summary>
    void OnDisable()
    {
        // コンピュートバッファは明示的に破棄しないと怒られます
        bulletsBuffer.Release();
    }

    Vector3 firstPosition,
        firstRotation;

    float x = 1.0f, y = 1.15f, z = 20.0f;

    /// <summary>
    /// 初期化
    /// </summary>
    void Start()
    {
        bulletsMaterial = new Material(bulletsShader);
        InitializeComputeBuffer();

        firstPosition = mainCamera.transform.position;

    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        bulletsComputeShader.SetBuffer(0, "Bullets", bulletsBuffer);
        bulletsComputeShader.SetFloat("DeltaTime", Time.deltaTime);
        bulletsComputeShader.Dispatch(0, bulletsBuffer.count / 8 + 1, 1, 1);
        if (Input.GetKeyDown(KeyCode.F1)) {
            InitializeComputeBuffer();
            mainCamera.transform.position = firstPosition;

        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            InitializeComputeBuffer2();
            mainCamera.transform.position= new Vector3(-50,0,0);
            mainCamera.transform.rotation = Quaternion.Euler(0, 96, 316);
        }else if (Input.GetKeyDown(KeyCode.F3))
        {
            InitializeComputeBuffer3();

        }else if (Input.GetKeyDown(KeyCode.F4))
        {
            InitializeComputeBuffer4();
        }

    }

    /// <summary>
    /// コンピュートバッファの初期化
    /// </summary>
    void InitializeComputeBuffer()
    {
        // 弾数は1万個
        bulletsBuffer = new ComputeBuffer(10000, Marshal.SizeOf(typeof(Bullet)));

        // 配列に初期値を代入する
        Bullet[] bullets = new Bullet[bulletsBuffer.count];
        for (int i = 0; i < bulletsBuffer.count; i++)
        {
            bullets[i] =
                new Bullet(
                    new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f)),
                    new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) * 0.5f,
                    new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)));
        }

        // バッファに適応
        bulletsBuffer.SetData(bullets);
    }

    void InitializeComputeBuffer2()
    {
        // 弾数は1万個
        bulletsBuffer = new ComputeBuffer(10000, Marshal.SizeOf(typeof(Bullet)));

        // 配列に初期値を代入する
        Bullet[] bullets = new Bullet[bulletsBuffer.count];
        for (int i = 0; i < bulletsBuffer.count; i++)
        {
            bullets[i] =
                new Bullet(
                    new Vector3(Random.Range(-10.0f, 10.0f*50), Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f)),
                    new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) * 0.5f,
                    new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)));
        }

        // バッファに適応
        bulletsBuffer.SetData(bullets);
    }

    void InitializeComputeBuffer3()
    {
        // 弾数は1万個
        bulletsBuffer = new ComputeBuffer(10000, Marshal.SizeOf(typeof(Bullet)));

        // 配列に初期値を代入する
        Bullet[] bullets = new Bullet[bulletsBuffer.count];
        for (int i = 0; i < bulletsBuffer.count; i++)
        {
            bullets[i] =
                new Bullet(
                    new Vector3(Mathf.Sin(Time.time), Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f)),
                    new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) * 50,
                    new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)));
        }

        // バッファに適応
        bulletsBuffer.SetData(bullets);
    }

    void InitializeComputeBuffer4()
    {
        // 弾数は1万個
        bulletsBuffer = new ComputeBuffer(100000, Marshal.SizeOf(typeof(Bullet)));

        // 配列に初期値を代入する
        Bullet[] bullets = new Bullet[bulletsBuffer.count];

        mainCamera.transform.position = new Vector3(-57, 4.7f, 5.8f);
        mainCamera.transform.rotation = Quaternion.Euler(357, 78, 324);


        for (int i = 0; i < bulletsBuffer.count; i++)
        {



            float xAnswer = x + 0.0001f * 10 * (-x + y);
            float yAnswer = y + 0.0001f * (28 * x - y - x * z);
            float zAnswer = z + 0.0001f * (-8.0f/3.0f * z + x * y);

            bullets[i] =

                new Bullet(
                    new Vector3(xAnswer, yAnswer, zAnswer),
                    new Vector3(Random.Range(-1.0f, 1.0f)*0.3f, Random.Range(-1.0f, 1.0f)*0.3f, Random.Range(-1.0f, 1.0f)) * 0.3f,
                    new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)));

            
            x = xAnswer;
            y = yAnswer;
            z = zAnswer;

        }

     


        // バッファに適応
        bulletsBuffer.SetData(bullets);


    }
    /// <summary>
    /// レンダリング
    /// </summary>
    void OnRenderObject()
    {

        // テクスチャ、バッファをマテリアルに設定
        bulletsMaterial.SetTexture("_MainTex", bulletsTexture);
        bulletsMaterial.SetBuffer("Bullets", bulletsBuffer);

        // レンダリングを開始
        bulletsMaterial.SetPass(0);

        // 1万個のオブジェクトをレンダリング
        Graphics.DrawProcedural(MeshTopology.Points, bulletsBuffer.count);
    }

}