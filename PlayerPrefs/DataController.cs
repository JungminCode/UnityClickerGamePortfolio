using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public class DataController : MonoBehaviour
{
    private static DataController instance; // 싱글턴 패턴으로 만들어 주기 위해, static을 붙여, 클래스 객체를 하나로 사용할 수 있게끔 만들어줌.

    DateTime mGetLastPlayDate() // 종료 했을 때의 시간을 전달하기 위한 DateTime 구조체형 함수
    {
        if (!PlayerPrefs.HasKey("Time"))    // Time이란 키가 없다면, 현재의 시간을 리턴한다.
        {
            return DateTime.Now;
        }
        string mTimeBinaryInString = PlayerPrefs.GetString("Time"); // Time으로 저장한 값을, string형 변수에 대입
        long mTimeBinaryInLong = Convert.ToInt64(mTimeBinaryInString);  // 그 스트링형 변수를 long형으로 변환 후 대입
        return DateTime.FromBinary(mTimeBinaryInLong);  // long형으로 되어 있는 값을 가공하여, DateTime으로 변환한 다음 리턴
    }

    void UpdateLastPlayDate()   // 종료 했을 때, Time이라는 키로 저장을 하고, 현재의 시간을 이진 값으로 문자열로 저장한다.
    {
        PlayerPrefs.SetString("Time", DateTime.Now.ToBinary().ToString());
    }

    void OnApplicationQuit()    // 종료했을 때,
    {
        UpdateLastPlayDate();   // 종료했을 때 시간을 저장하는 함수를 실행
    }

    public long mGold   // 골드 프로퍼티
    {
        get
        {
            if (!PlayerPrefs.HasKey("Gold"))    // Gold로 저장된 값이 없다면, 0을 리턴한다.
            {
                return 0;
            }
            string mTempGold = PlayerPrefs.GetString("Gold");   // 있다면, 그 저장된 값을 문자열로 대입
            return long.Parse(mTempGold);   // 문자열에 대입한 값을 long형으로 변환
        }
        set
        {
            PlayerPrefs.SetString("Gold", value.ToString());    // "Gold"라는 키로 현재의 gold값을 저장
        }
    }

    public long mCheque // 수표 프로퍼티
    {
        get
        {
            if (!PlayerPrefs.HasKey("Cheque"))  // 골드와 마찬가지로, Cheque로 저장된 값이 없다면, 0을 리턴
            {
                return 0;
            }
            string mTempCheque = PlayerPrefs.GetString("Cheque");   // 있다면, Cheque로 저장 된 값을 문자열로 대입
            return long.Parse(mTempCheque); // 그 값을 long형으로 변환
        }
        set
        {
            PlayerPrefs.SetString("Cheque", value.ToString());  // "Cheque"라는 키로, 현재의 수표를 저장
        }
    }

    public long mGoldPerClick   // 클릭 당 골드 프로퍼티
    {
        get
        {
            if (!PlayerPrefs.HasKey("GoldPerClick"))    // GoldPerClick의 키로 저장된 게 없다면, 1을 리턴
            {
                return 1;
            }
            string mTempGold = PlayerPrefs.GetString("GoldPerClick");   // "GoldPerClick"으로 저장 된 값을 문자열로 대입
            return long.Parse(mTempGold);   // 그 문자열을 long형으로 변환
        }
        set
        {
            PlayerPrefs.SetString("GoldPerClick", value.ToString());    // 클릭 당 골드를 GoldPerClick이라는 키로 저장후, 클릭 당 골드를 저장
        }
    }

    public static DataController Instance   // 싱글톤 패턴으로 만들기
    {
        get
        {
            if (instance == null)   // instance가 null이면,
            {
                instance = FindObjectOfType<DataController>();  // instance변수에, DataController 클래스를 대입

                if (instance == null)   // instance가 null이면,
                {
                    GameObject container = new GameObject("DataController");    // DataController라는 게임오브젝트를 하나 생성
                    instance = container.AddComponent<DataController>();    // 그 생성된 게임오브젝트에, DataController 스크립트를 컴포넌트로 넣어준다.
                }
            }

            return instance;    // instance를 리턴한다.
        }
    }

    public int mTimeAfterLastPlay   // 종료한 시간과 다시 게임을 시작한 시간을 감산해주는 변수
    {
        get
        {
            DateTime mCurrentTime = DateTime.Now;   // 현재 시간을 대입
            DateTime mLastPlayDate = mGetLastPlayDate();    // 마지막 시간을 대입
            return (int)mCurrentTime.Subtract(mLastPlayDate).TotalSeconds;  // 현재 시간에, 마지막으로 종료한 시간을 감산하여 초 단위로 int형으로 캐스팅 후, 리턴한다. 
        }
    }

    // 프로퍼티 끝!


    // 함수 시작 부분


    private ProductUpgradeButton [] mProductButtons; // 제품 버튼 스크립트 배열을 생성

    private void Awake()
    {
        mProductButtons = FindObjectsOfType<ProductUpgradeButton>();    // ProductUpgradeButton을 가지고 있는 오브젝트들을 배열안에 전부 넣어준다.
    }

    private void Start()
    {
        mGold += GetGoldPerSec() * mTimeAfterLastPlay;  // 시작할 때, 종료한 뒤 현재 들어 온 시간에 감산한 값과 초 당 골드를 곱한 값을 골드에 넣어준다.
        InvokeRepeating("UpdateLastPlayDate", 0f, 5f);  // UpdateLastPlayDate함수를, 시작하고, 5초주기마다 실행시켜준다.
    }

    // 회사 업그레이드 부분

    public void LoadCompanyUpGradeButton(UpGradeButton mUpgradeButton)  // 회사에 관한 정보를 가져오는 함수
    {
        string key = mUpgradeButton.mUpGradeCompanyName;    // 각각의 회사 이름을 가져와 대입한다.

        mUpgradeButton.mLevel = PlayerPrefs.GetInt(key + "_level", mUpgradeButton.mLevel);  // 각각 회사의 level을 읽어온다.
        mUpgradeButton.mGoldByUpgrade = PlayerPrefsX.GetLong(key + "_goldByUpgrade", mUpgradeButton.mStartGoldByUpGrade);   // 각각 회사의 클릭 당 골드를 불러온다.
        mUpgradeButton.mCurrentCost = PlayerPrefsX.GetLong(key + "_cost", mUpgradeButton.mStartCurrentCost);    // 회사 업그레이드에 필요한 값을 불러온다.
    }
    
    public void SaveCompanyUpGradeButton(UpGradeButton mUpgradeButton)  // 회사에 관한 정보를 저장하는 함수
    {
        string key = mUpgradeButton.mUpGradeCompanyName;    // 각각 회사의 이름을 가져와 대입

        PlayerPrefs.SetInt(key + "_level", mUpgradeButton.mLevel);  // 각 회사의 level을 저장
        PlayerPrefsX.SetLong(key + "_goldByUpgrade", mUpgradeButton.mGoldByUpgrade);    // 각 회사의 클릭당 골드를 저장
        PlayerPrefsX.SetLong(key + "_cost", mUpgradeButton.mCurrentCost);   // 각 회사의 업그레이드 비용을 저장
    }

    // 회사 업그레이드 끝!


    // 제품 업그레이드 부분

    public void LoadProductUpGradeButton(ProductUpgradeButton mProductButton)   // 제품 저장 함수
    {
        string key = mProductButton.mUpGradeProductName;    // 제품의 각각 이름을 대입

        mProductButton.mLevel = PlayerPrefs.GetInt(key + "_level"); // 제품의 level을 불러옴
        mProductButton.mGoldPerSecByUpgrade = PlayerPrefsX.GetLong(key + "_goldPerSec");    // 제품의 초당 골드를 불러옴
        mProductButton.mCurrentProductCost = PlayerPrefsX.GetLong(key + "_cost", mProductButton.mStartCurrentProductCost);  // 제품의 업그레이드 비용을 불러옴
        if (PlayerPrefs.GetInt(key + "_isPurchased") == 1)  // 제품이 업그레이드가 되었는지 안되었는지 확인한다.
        {
            mProductButton.mIsPurchased = true; //  업그레이드가 되어 있다면, true로 바꾸어준다.
        }
        else
        {
            mProductButton.mIsPurchased = false;    // 안되어 있다면, false값으로 바꾸어준다.
        }
    }

    // 제품 Save
    public void SaveProductUpGradeButton(ProductUpgradeButton mProductButton)   // 제품의 정보를 저장하는 함수
    {
        string key = mProductButton.mUpGradeProductName;    // 제품의 이름을 가져온다.

        PlayerPrefs.SetInt(key + "_level", mProductButton.mLevel);  // 제품의 레벨을 저장한다.
        PlayerPrefsX.SetLong(key + "_goldPerSec", mProductButton.mGoldPerSecByUpgrade); // 제품의 초 당 골드를 저장한다.
        PlayerPrefsX.SetLong(key + "_cost", mProductButton.mCurrentProductCost);    // 제품의 업그레이드 비용을 저장한다.

        if (mProductButton.mIsPurchased == true)    // 제품이 한번이라도 업그레이드가 됐다면,
        {
            PlayerPrefs.SetInt(key + "_isPurchased", 1);    // int형으로 1로 저장한다. key값은 제품의 이름 + "_isPurchased"라는 것으로
        }
        else
        {
            PlayerPrefs.SetInt(key + "_isPurchased", 0);    // 업그레이드 안되어있다면, 0으로 저장
        }
    }

    public long GetGoldPerSec() // 초 당 골드를 구하기 위한 함수
    {
        long mGoldPerSecSave = 0;   // long형의 변수를 0으로 초기화해준다.
        for (int i = 0; i < mProductButtons.Length; i++)    // Awake함수에서, 만들어준 변수를 for문을 이용해, 그 제품들을 확인한다.
        {
            if (mProductButtons[i].mIsPurchased == true)    // 업그레이드가 되어 있는 제품이라면,
            {
                mGoldPerSecSave += mProductButtons[i].mGoldPerSecByUpgrade; // 그 제품의 초 당 골드를 mGoldPerSecSave변수에 저장
            }
        }

        return mGoldPerSecSave; // 초 당 골드 값을 리턴
    }

    // 사운드 ON, OFF 및 진동 ON, OFF
    
    public void LoadSound(ButtonManager mButtonManager) // 사운드의 볼륨값을 불러오는 함수
    {
        mButtonManager.mCurrentSoundVolume = PlayerPrefs.GetFloat("_volume", mButtonManager.mSoundStartVolume); // 현재 볼륨을 불러온다.
    }

    public void SaveSound(ButtonManager mButtonManager) // 사운드의 볼륨값을 저장하는 함수
    {
        PlayerPrefs.SetFloat("_volume", mButtonManager.mCurrentSoundVolume);    // 현재 볼륨을 저장한다.
    }

    public void LoadVive(ButtonManager mButtonManager)  // 현재 진동이 ON,OFF인지를 불러온다.
    {
        mButtonManager.mCurrentVive = PlayerPrefsX.GetBool("_vive",mButtonManager.mStartVive);  // 현재의 진동이 ON,OFF인지를 불러온다.
    } 

    public void SaveVive(ButtonManager mButtonManager)  // 현재 진동을 저장한다.
    {
        PlayerPrefsX.SetBool("_vive", mButtonManager.mCurrentVive); // 진동이 ON,OFF인지를 저장한다.
    }

    // 함수 끝
}




























// PlayerPrefsX 클래스 부분

public class PlayerPrefsX
{
    static private int endianDiff1;
    static private int endianDiff2;
    static private int idx;
    static private byte[] byteBlock;

    enum ArrayType { Float, Int32, Bool, String, Vector2, Vector3, Quaternion, Color }

    public static bool SetBool(String name, bool value)
    {
        try
        {
            PlayerPrefs.SetInt(name, value ? 1 : 0);
        }
        catch
        {
            return false;
        }
        return true;
    }

    public static bool GetBool(String name)
    {
        return PlayerPrefs.GetInt(name) == 1;
    }

    public static bool GetBool(String name, bool defaultValue)
    {
        return (1 == PlayerPrefs.GetInt(name, defaultValue ? 1 : 0));
    }

    public static long GetLong(string key, long defaultValue)
    {
        int lowBits, highBits;
        SplitLong(defaultValue, out lowBits, out highBits);
        lowBits = PlayerPrefs.GetInt(key + "_lowBits", lowBits);
        highBits = PlayerPrefs.GetInt(key + "_highBits", highBits);

        // unsigned, to prevent loss of sign bit.
        ulong ret = (uint)highBits;
        ret = (ret << 32);
        return (long)(ret | (ulong)(uint)lowBits);
    }

    public static long GetLong(string key)
    {
        int lowBits = PlayerPrefs.GetInt(key + "_lowBits");
        int highBits = PlayerPrefs.GetInt(key + "_highBits");

        // unsigned, to prevent loss of sign bit.
        ulong ret = (uint)highBits;
        ret = (ret << 32);
        return (long)(ret | (ulong)(uint)lowBits);
    }

    private static void SplitLong(long input, out int lowBits, out int highBits)
    {
        // unsigned everything, to prevent loss of sign bit.
        lowBits = (int)(uint)(ulong)input;
        highBits = (int)(uint)(input >> 32);
    }

    public static void SetLong(string key, long value)
    {
        int lowBits, highBits;
        SplitLong(value, out lowBits, out highBits);
        PlayerPrefs.SetInt(key + "_lowBits", lowBits);
        PlayerPrefs.SetInt(key + "_highBits", highBits);
    }

    public static bool SetVector2(String key, Vector2 vector)
    {
        return SetFloatArray(key, new float[] { vector.x, vector.y });
    }

    static Vector2 GetVector2(String key)
    {
        var floatArray = GetFloatArray(key);
        if (floatArray.Length < 2)
        {
            return Vector2.zero;
        }
        return new Vector2(floatArray[0], floatArray[1]);
    }

    public static Vector2 GetVector2(String key, Vector2 defaultValue)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return GetVector2(key);
        }
        return defaultValue;
    }

    public static bool SetVector3(String key, Vector3 vector)
    {
        return SetFloatArray(key, new float[] { vector.x, vector.y, vector.z });
    }

    public static Vector3 GetVector3(String key)
    {
        var floatArray = GetFloatArray(key);
        if (floatArray.Length < 3)
        {
            return Vector3.zero;
        }
        return new Vector3(floatArray[0], floatArray[1], floatArray[2]);
    }

    public static Vector3 GetVector3(String key, Vector3 defaultValue)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return GetVector3(key);
        }
        return defaultValue;
    }

    public static bool SetQuaternion(String key, Quaternion vector)
    {
        return SetFloatArray(key, new float[] { vector.x, vector.y, vector.z, vector.w });
    }

    public static Quaternion GetQuaternion(String key)
    {
        var floatArray = GetFloatArray(key);
        if (floatArray.Length < 4)
        {
            return Quaternion.identity;
        }
        return new Quaternion(floatArray[0], floatArray[1], floatArray[2], floatArray[3]);
    }

    public static Quaternion GetQuaternion(String key, Quaternion defaultValue)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return GetQuaternion(key);
        }
        return defaultValue;
    }

    public static bool SetColor(String key, Color color)
    {
        return SetFloatArray(key, new float[] { color.r, color.g, color.b, color.a });
    }

    public static Color GetColor(String key)
    {
        var floatArray = GetFloatArray(key);
        if (floatArray.Length < 4)
        {
            return new Color(0.0f, 0.0f, 0.0f, 0.0f);
        }
        return new Color(floatArray[0], floatArray[1], floatArray[2], floatArray[3]);
    }

    public static Color GetColor(String key, Color defaultValue)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return GetColor(key);
        }
        return defaultValue;
    }

    public static bool SetBoolArray(String key, bool[] boolArray)
    {
        // Make a byte array that's a multiple of 8 in length, plus 5 bytes to store the number of entries as an int32 (+ identifier)
        // We have to store the number of entries, since the boolArray length might not be a multiple of 8, so there could be some padded zeroes
        var bytes = new byte[(boolArray.Length + 7) / 8 + 5];
        bytes[0] = System.Convert.ToByte(ArrayType.Bool);   // Identifier
        var bits = new BitArray(boolArray);
        bits.CopyTo(bytes, 5);
        Initialize();
        ConvertInt32ToBytes(boolArray.Length, bytes); // The number of entries in the boolArray goes in the first 4 bytes

        return SaveBytes(key, bytes);
    }

    public static bool[] GetBoolArray(String key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            var bytes = System.Convert.FromBase64String(PlayerPrefs.GetString(key));
            if (bytes.Length < 5)
            {
                Debug.LogError("Corrupt preference file for " + key);
                return new bool[0];
            }
            if ((ArrayType)bytes[0] != ArrayType.Bool)
            {
                Debug.LogError(key + " is not a boolean array");
                return new bool[0];
            }
            Initialize();

            // Make a new bytes array that doesn't include the number of entries + identifier (first 5 bytes) and turn that into a BitArray
            var bytes2 = new byte[bytes.Length - 5];
            System.Array.Copy(bytes, 5, bytes2, 0, bytes2.Length);
            var bits = new BitArray(bytes2);
            // Get the number of entries from the first 4 bytes after the identifier and resize the BitArray to that length, then convert it to a boolean array
            bits.Length = ConvertBytesToInt32(bytes);
            var boolArray = new bool[bits.Count];
            bits.CopyTo(boolArray, 0);

            return boolArray;
        }
        return new bool[0];
    }

    public static bool[] GetBoolArray(String key, bool defaultValue, int defaultSize)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return GetBoolArray(key);
        }
        var boolArray = new bool[defaultSize];
        for (int i = 0; i < defaultSize; i++)
        {
            boolArray[i] = defaultValue;
        }
        return boolArray;
    }

    public static bool SetStringArray(String key, String[] stringArray)
    {
        var bytes = new byte[stringArray.Length + 1];
        bytes[0] = System.Convert.ToByte(ArrayType.String); // Identifier
        Initialize();

        // Store the length of each string that's in stringArray, so we can extract the correct strings in GetStringArray
        for (var i = 0; i < stringArray.Length; i++)
        {
            if (stringArray[i] == null)
            {
                Debug.LogError("Can't save null entries in the string array when setting " + key);
                return false;
            }
            if (stringArray[i].Length > 255)
            {
                Debug.LogError("Strings cannot be longer than 255 characters when setting " + key);
                return false;
            }
            bytes[idx++] = (byte)stringArray[i].Length;
        }

        try
        {
            PlayerPrefs.SetString(key, System.Convert.ToBase64String(bytes) + "|" + String.Join("", stringArray));
        }
        catch
        {
            return false;
        }
        return true;
    }

    public static String[] GetStringArray(String key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            var completeString = PlayerPrefs.GetString(key);
            var separatorIndex = completeString.IndexOf("|"[0]);
            if (separatorIndex < 4)
            {
                Debug.LogError("Corrupt preference file for " + key);
                return new String[0];
            }
            var bytes = System.Convert.FromBase64String(completeString.Substring(0, separatorIndex));
            if ((ArrayType)bytes[0] != ArrayType.String)
            {
                Debug.LogError(key + " is not a string array");
                return new String[0];
            }
            Initialize();

            var numberOfEntries = bytes.Length - 1;
            var stringArray = new String[numberOfEntries];
            var stringIndex = separatorIndex + 1;
            for (var i = 0; i < numberOfEntries; i++)
            {
                int stringLength = bytes[idx++];
                if (stringIndex + stringLength > completeString.Length)
                {
                    Debug.LogError("Corrupt preference file for " + key);
                    return new String[0];
                }
                stringArray[i] = completeString.Substring(stringIndex, stringLength);
                stringIndex += stringLength;
            }

            return stringArray;
        }
        return new String[0];
    }

    public static String[] GetStringArray(String key, String defaultValue, int defaultSize)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return GetStringArray(key);
        }
        var stringArray = new String[defaultSize];
        for (int i = 0; i < defaultSize; i++)
        {
            stringArray[i] = defaultValue;
        }
        return stringArray;
    }

    public static bool SetIntArray(String key, int[] intArray)
    {
        return SetValue(key, intArray, ArrayType.Int32, 1, ConvertFromInt);
    }

    public static bool SetFloatArray(String key, float[] floatArray)
    {
        return SetValue(key, floatArray, ArrayType.Float, 1, ConvertFromFloat);
    }

    public static bool SetVector2Array(String key, Vector2[] vector2Array)
    {
        return SetValue(key, vector2Array, ArrayType.Vector2, 2, ConvertFromVector2);
    }

    public static bool SetVector3Array(String key, Vector3[] vector3Array)
    {
        return SetValue(key, vector3Array, ArrayType.Vector3, 3, ConvertFromVector3);
    }

    public static bool SetQuaternionArray(String key, Quaternion[] quaternionArray)
    {
        return SetValue(key, quaternionArray, ArrayType.Quaternion, 4, ConvertFromQuaternion);
    }

    public static bool SetColorArray(String key, Color[] colorArray)
    {
        return SetValue(key, colorArray, ArrayType.Color, 4, ConvertFromColor);
    }

    private static bool SetValue<T>(String key, T array, ArrayType arrayType, int vectorNumber, Action<T, byte[], int> convert) where T : IList
    {
        var bytes = new byte[(4 * array.Count) * vectorNumber + 1];
        bytes[0] = System.Convert.ToByte(arrayType);    // Identifier
        Initialize();

        for (var i = 0; i < array.Count; i++)
        {
            convert(array, bytes, i);
        }
        return SaveBytes(key, bytes);
    }

    private static void ConvertFromInt(int[] array, byte[] bytes, int i)
    {
        ConvertInt32ToBytes(array[i], bytes);
    }

    private static void ConvertFromFloat(float[] array, byte[] bytes, int i)
    {
        ConvertFloatToBytes(array[i], bytes);
    }

    private static void ConvertFromVector2(Vector2[] array, byte[] bytes, int i)
    {
        ConvertFloatToBytes(array[i].x, bytes);
        ConvertFloatToBytes(array[i].y, bytes);
    }

    private static void ConvertFromVector3(Vector3[] array, byte[] bytes, int i)
    {
        ConvertFloatToBytes(array[i].x, bytes);
        ConvertFloatToBytes(array[i].y, bytes);
        ConvertFloatToBytes(array[i].z, bytes);
    }

    private static void ConvertFromQuaternion(Quaternion[] array, byte[] bytes, int i)
    {
        ConvertFloatToBytes(array[i].x, bytes);
        ConvertFloatToBytes(array[i].y, bytes);
        ConvertFloatToBytes(array[i].z, bytes);
        ConvertFloatToBytes(array[i].w, bytes);
    }

    private static void ConvertFromColor(Color[] array, byte[] bytes, int i)
    {
        ConvertFloatToBytes(array[i].r, bytes);
        ConvertFloatToBytes(array[i].g, bytes);
        ConvertFloatToBytes(array[i].b, bytes);
        ConvertFloatToBytes(array[i].a, bytes);
    }

    public static int[] GetIntArray(String key)
    {
        var intList = new List<int>();
        GetValue(key, intList, ArrayType.Int32, 1, ConvertToInt);
        return intList.ToArray();
    }

    public static int[] GetIntArray(String key, int defaultValue, int defaultSize)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return GetIntArray(key);
        }
        var intArray = new int[defaultSize];
        for (int i = 0; i < defaultSize; i++)
        {
            intArray[i] = defaultValue;
        }
        return intArray;
    }

    public static float[] GetFloatArray(String key)
    {
        var floatList = new List<float>();
        GetValue(key, floatList, ArrayType.Float, 1, ConvertToFloat);
        return floatList.ToArray();
    }

    public static float[] GetFloatArray(String key, float defaultValue, int defaultSize)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return GetFloatArray(key);
        }
        var floatArray = new float[defaultSize];
        for (int i = 0; i < defaultSize; i++)
        {
            floatArray[i] = defaultValue;
        }
        return floatArray;
    }

    public static Vector2[] GetVector2Array(String key)
    {
        var vector2List = new List<Vector2>();
        GetValue(key, vector2List, ArrayType.Vector2, 2, ConvertToVector2);
        return vector2List.ToArray();
    }

    public static Vector2[] GetVector2Array(String key, Vector2 defaultValue, int defaultSize)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return GetVector2Array(key);
        }
        var vector2Array = new Vector2[defaultSize];
        for (int i = 0; i < defaultSize; i++)
        {
            vector2Array[i] = defaultValue;
        }
        return vector2Array;
    }

    public static Vector3[] GetVector3Array(String key)
    {
        var vector3List = new List<Vector3>();
        GetValue(key, vector3List, ArrayType.Vector3, 3, ConvertToVector3);
        return vector3List.ToArray();
    }

    public static Vector3[] GetVector3Array(String key, Vector3 defaultValue, int defaultSize)
    {
        if (PlayerPrefs.HasKey(key))

        {
            return GetVector3Array(key);
        }
        var vector3Array = new Vector3[defaultSize];
        for (int i = 0; i < defaultSize; i++)
        {
            vector3Array[i] = defaultValue;
        }
        return vector3Array;
    }

    public static Quaternion[] GetQuaternionArray(String key)
    {
        var quaternionList = new List<Quaternion>();
        GetValue(key, quaternionList, ArrayType.Quaternion, 4, ConvertToQuaternion);
        return quaternionList.ToArray();
    }

    public static Quaternion[] GetQuaternionArray(String key, Quaternion defaultValue, int defaultSize)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return GetQuaternionArray(key);
        }
        var quaternionArray = new Quaternion[defaultSize];
        for (int i = 0; i < defaultSize; i++)
        {
            quaternionArray[i] = defaultValue;
        }
        return quaternionArray;
    }

    public static Color[] GetColorArray(String key)
    {
        var colorList = new List<Color>();
        GetValue(key, colorList, ArrayType.Color, 4, ConvertToColor);
        return colorList.ToArray();
    }

    public static Color[] GetColorArray(String key, Color defaultValue, int defaultSize)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return GetColorArray(key);
        }
        var colorArray = new Color[defaultSize];
        for (int i = 0; i < defaultSize; i++)
        {
            colorArray[i] = defaultValue;
        }
        return colorArray;
    }

    private static void GetValue<T>(string key, T list, ArrayType arrayType, int vectorNumber, Action<T, byte[]> convert) where T : IList
    {
        if (PlayerPrefs.HasKey(key))
        {
            var bytes = System.Convert.FromBase64String(PlayerPrefs.GetString(key));
            if ((bytes.Length - 1) % (vectorNumber * 4) != 0)
            {
                Debug.LogError("Corrupt preference file for " + key);
                return;
            }
            if ((ArrayType)bytes[0] != arrayType)
            {
                Debug.LogError(key + " is not a " + arrayType.ToString() + " array");
                return;
            }
            Initialize();

            var end = (bytes.Length - 1) / (vectorNumber * 4);
            for (var i = 0; i < end; i++)
            {
                convert(list, bytes);
            }
        }
    }

    private static void ConvertToInt(List<int> list, byte[] bytes)
    {
        list.Add(ConvertBytesToInt32(bytes));
    }

    private static void ConvertToFloat(List<float> list, byte[] bytes)
    {
        list.Add(ConvertBytesToFloat(bytes));
    }

    private static void ConvertToVector2(List<Vector2> list, byte[] bytes)
    {
        list.Add(new Vector2(ConvertBytesToFloat(bytes), ConvertBytesToFloat(bytes)));
    }

    private static void ConvertToVector3(List<Vector3> list, byte[] bytes)
    {
        list.Add(new Vector3(ConvertBytesToFloat(bytes), ConvertBytesToFloat(bytes), ConvertBytesToFloat(bytes)));
    }

    private static void ConvertToQuaternion(List<Quaternion> list, byte[] bytes)
    {
        list.Add(new Quaternion(ConvertBytesToFloat(bytes), ConvertBytesToFloat(bytes), ConvertBytesToFloat(bytes), ConvertBytesToFloat(bytes)));
    }

    private static void ConvertToColor(List<Color> list, byte[] bytes)
    {
        list.Add(new Color(ConvertBytesToFloat(bytes), ConvertBytesToFloat(bytes), ConvertBytesToFloat(bytes), ConvertBytesToFloat(bytes)));
    }

    public static void ShowArrayType(string key)
    {
        var bytes = System.Convert.FromBase64String(PlayerPrefs.GetString(key));
        if (bytes.Length > 0)
        {
            ArrayType arrayType = (ArrayType)bytes[0];
            Debug.Log(key + " is a " + arrayType.ToString() + " array");
        }
    }

    private static void Initialize()
    {
        if (System.BitConverter.IsLittleEndian)
        {
            endianDiff1 = 0;
            endianDiff2 = 0;
        }
        else
        {
            endianDiff1 = 3;
            endianDiff2 = 1;
        }
        if (byteBlock == null)
        {
            byteBlock = new byte[4];
        }
        idx = 1;
    }

    private static bool SaveBytes(string key, byte[] bytes)
    {
        try
        {
            PlayerPrefs.SetString(key, System.Convert.ToBase64String(bytes));
        }
        catch
        {
            return false;
        }
        return true;
    }

    private static void ConvertFloatToBytes(float f, byte[] bytes)
    {
        byteBlock = System.BitConverter.GetBytes(f);
        ConvertTo4Bytes(bytes);
    }

    private static float ConvertBytesToFloat(byte[] bytes)
    {
        ConvertFrom4Bytes(bytes);
        return System.BitConverter.ToSingle(byteBlock, 0);
    }

    private static void ConvertInt32ToBytes(int i, byte[] bytes)
    {
        byteBlock = System.BitConverter.GetBytes(i);
        ConvertTo4Bytes(bytes);
    }

    private static int ConvertBytesToInt32(byte[] bytes)
    {
        ConvertFrom4Bytes(bytes);
        return System.BitConverter.ToInt32(byteBlock, 0);
    }

    private static void ConvertTo4Bytes(byte[] bytes)
    {
        bytes[idx] = byteBlock[endianDiff1];
        bytes[idx + 1] = byteBlock[1 + endianDiff2];
        bytes[idx + 2] = byteBlock[2 - endianDiff2];
        bytes[idx + 3] = byteBlock[3 - endianDiff1];
        idx += 4;
    }

    private static void ConvertFrom4Bytes(byte[] bytes)
    {
        byteBlock[endianDiff1] = bytes[idx];
        byteBlock[1 + endianDiff2] = bytes[idx + 1];
        byteBlock[2 - endianDiff2] = bytes[idx + 2];
        byteBlock[3 - endianDiff1] = bytes[idx + 3];
        idx += 4;
    }
}
