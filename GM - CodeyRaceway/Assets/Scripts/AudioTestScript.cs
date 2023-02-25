using UnityEngine;

public enum ESamplingSize
{
    MIN_64 = 64,
    S_128 = 128,
    S_256 = 256,
    S_512 = 512,
    S_1024 = 1024,
    S_2048 = 2048,
}

public class AudioTestScript : MonoBehaviour
{
    [Header("Initial Settings")]
    public Sprite audioVisSprite;
    public Sprite audioVisBassSprite;
    [Tooltip("Sample size needs to be power of 2 starting with a MIN of 64 and MAX of 8192")]
    public ESamplingSize sampleSizeRef;
    public float visSpacing;

    [Header("Scale-Audio Manipulation")]
    public float visScale;
    public float visBassScale;
    public float visBassMinRadius;
    public float visBassRadiusInc;
    public float visBarWidth;
    public float visBarHeight;

    [Header("Colour-Audio Manipulation")]
    public Color baseColour = Color.black;
    public Color lowBassColour = Color.white;
    public Color highBassColour = Color.black;
    public Color lowFreqColour = Color.white;
    public Color midFreqColour = Color.black;
    public Color highFreqColour = Color.black;


    AudioSource audio;
    GameObject[] visualizers;
    GameObject[] bassVisualizers;
    float[] samples;
    Vector3[] bassObjOriginalScale;
    int sampleSize;
    int bassSection;
    

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        sampleSize = (int)sampleSizeRef;
        samples = new float[sampleSize];

        //Value checker
        if (visSpacing == 0f) visSpacing = 0.1f;
        if (visScale == 0f) visScale = 1f;
        if (visBassScale == 0f) visBassScale = 1f;
        if (visBassMinRadius == 0f) visBassMinRadius = 1f;
        if (visBassRadiusInc == 0f) visBassRadiusInc = 0.5f;
        if (visBarWidth == 0f) visBarWidth = 0.1f;
        if (visBarHeight == 0f) visBarHeight = 0.1f;

        //Creating reference obj
        GameObject visObject = CreateVisualizer("No:", audioVisSprite);
        GameObject visBassObject = CreateVisualizer("Bass:", audioVisBassSprite);


        SplitAndCreate(visObject, visBassObject);


        //Delete the reference obj
        GameObject.Destroy(visObject);
        GameObject.Destroy(visBassObject);
    }

    // Update is called once per frame
    void Update()
    {
        TestAudio();
    }

    //This function doesn't need to be reusable/decoupled, let it make whatever object I need...
    GameObject CreateVisualizer(string _name, Sprite _sprite)
    {
        //Create new object with the following components
        GameObject newObject = new GameObject(_name, typeof(SpriteRenderer));

        //Set and initialize components
        newObject.GetComponent<SpriteRenderer>().sprite = _sprite;

        return newObject;
    }

    GameObject[] SpawnInLine(GameObject _object, int _numToSpawn, float _spacing)
    {
        GameObject[] objects = new GameObject[_numToSpawn];
        string baseName = _object.name;

        //Offset calc
        float offset = (float)(_numToSpawn - 1) * _spacing * 0.5f;

        Vector3 position = Vector3.zero;
        
        for (int i = 0; i < _numToSpawn; i++)
        {
            //Basic math for placing object in a line
            position.x = (visSpacing * i) - offset;


            ///CREATING THE OBJECT & SETTING ITS POSITION
            objects[i] = Instantiate(_object, Vector3.zero, Quaternion.identity);
            objects[i].name = baseName + i;
            objects[i].transform.parent = transform;
            objects[i].transform.localPosition = position;
        }

        return objects;
    }

    GameObject[] SpawnInCircle(GameObject _object, int _numToSpawn, float _radius)
    {
        GameObject[] objects = new GameObject[_numToSpawn];
        string baseName = _object.name;

        //Offset calc, angular increments
        float angleInc = (2f*Mathf.PI) / (float)_numToSpawn;

        Vector3 position = Vector3.zero;
        Vector3 rotation = Vector3.zero;
        
        for(int i = 0; i < _numToSpawn; i++)
        {
            ///MATH FOR COORDINATES AROUND A CIRCLE
            //In this case, I'm placing the objects on the XY plane
            position.x = Mathf.Cos((float)i * angleInc);
            position.y = Mathf.Sin((float)i * angleInc);

            //Applying the radius adjustment
            position *= _radius;

            //Calculate angle orientation (rotation)
            rotation.z = ((float)i * angleInc) * Mathf.Rad2Deg;
            rotation.z += 90f;

            ///CREATING THE OBJECT & SETTING ITS POSITION
            objects[i] = Instantiate(_object, Vector3.zero, Quaternion.identity);
            objects[i].name = baseName + i;
            objects[i].transform.parent = transform;
            objects[i].transform.localPosition = position;
            objects[i].transform.localRotation = Quaternion.Euler(rotation);
        }

        return objects;
    }

    GameObject[] SpawnInGrid(GameObject _object, int _row, int _col, float _spacing)
    {
        GameObject[] objects = new GameObject[_row * _col];
        string baseName = _object.name;


        //Offsets for repositioning the grid where the CENTER of the grid is at 0,0 local space as supposed to BOTTOM LEFT where the grid start spawning
        float xOffset = (_row - 1) * _spacing * 0.5f;
        float yOffset = (_col - 1) * _spacing * 0.5f;

        Vector3 position = Vector3.zero;

        for (int i = 0; i < _col; i++)
        {
            for (int j = 0; j < _row; j++)
            {
                ///MATH FOR COORDINATES AROUND ON A GRID/TILE BASED
                position.x = (float)j * _spacing;
                position.y = (float)i * _spacing;

                //Apply the offset if we need the grid to be centered from the initial spawn point
                position.x -= xOffset; 
                position.y -= yOffset;


                ///CREATING THE OBJECT & SETTING ITS POSITION
                //Set and initialize components
                ///CREATING THE OBJECT & SETTING ITS POSITION
                objects[i] = Instantiate(_object, Vector3.zero, Quaternion.identity);
                objects[i].name = baseName + i;
                objects[i].transform.parent = transform;
                objects[i].transform.localPosition = position;
            }
        }

        return objects;
    }

    GameObject[] SpawnBassObjects(GameObject _object, int _numToSpawn, float _radiusRatio)
    {
        GameObject[] objects = new GameObject[_numToSpawn];
        bassObjOriginalScale = new Vector3[_numToSpawn];
        string baseName = _object.name;

        Vector3 position = Vector3.zero;
        Vector3 scale = Vector3.one;
        float minRadius = 1f;


        for (int i = 0; i < _numToSpawn; i++)
        {
            scale.x = ((float)i * _radiusRatio) + minRadius;
            scale.y = scale.x;
            position.z += (float)i * 0.1f;

            ///CREATING THE OBJECT & SETTING ITS POSITION
            objects[i] = Instantiate(_object, Vector3.zero, Quaternion.identity);
            objects[i].name = baseName + i;
            objects[i].transform.parent = transform;
            objects[i].transform.localPosition = position;
            objects[i].transform.localScale = scale;

            bassObjOriginalScale[i] = scale;
        }

        return objects;
    }

    void SplitAndCreate(GameObject _visBar, GameObject _visBass)
    {
        bassSection = (sampleSize / 64) + 3;


        bassVisualizers = SpawnBassObjects(_visBass, bassSection, visBassRadiusInc);
        visualizers = SpawnInCircle(_visBar, sampleSize - bassSection, visSpacing);
    }

    void TestAudio()
    {
        audio.GetSpectrumData(samples, 0, FFTWindow.Blackman);

        for (int i = 0; i < bassSection; i++)
        {
            ref GameObject vis = ref bassVisualizers[i];
            ref float sample = ref samples[i];

            ///Scale manipulation using values from audio data
            Vector3 newScale = bassObjOriginalScale[i];

            newScale.x += sample * visBassScale * (1f / (1f + sample));
            newScale.y = newScale.x;
            vis.transform.localScale = newScale;


            ///Colour manipulation using values from audio data
            Color newColor = Vector4.Lerp(lowBassColour, highBassColour, (float)i / (bassSection - 1f));

            newColor = sample * (0.1f * visBassScale)* newColor;
            newColor.a = 1f;
            vis.GetComponent<SpriteRenderer>().color = newColor;
        }

        int indexAdj = bassSection;

        float midIndex = (float)(sampleSize - bassSection) * 0.5f;

        for (int i = indexAdj; i < sampleSize; i++)
        {
            ref GameObject vis = ref visualizers[i - indexAdj];
            ref float sample = ref samples[i];

            ///Scale manipulation using values from audio data
            Vector3 newScale = new Vector3(visBarWidth, visBarHeight, 1f);

            newScale.y = visBarHeight + (sample * visScale);
            vis.transform.localScale = newScale;


            ///Colour manipulation using values from audio data
            Color newColor;

            if (i < midIndex)
            {
                float t = (float)(i - indexAdj) / midIndex;
                newColor = Vector4.Lerp(lowFreqColour, midFreqColour, t);
            }
            else
            {
                float t = (float)(i - indexAdj - midIndex) / midIndex;
                newColor = Vector4.Lerp(midFreqColour, highFreqColour, t);
            }

            newColor = (sample * (float)i * (0.005f * visScale)) * newColor;
            newColor.a = 1f;
            vis.GetComponent<SpriteRenderer>().color = newColor;
        }
    }
}
