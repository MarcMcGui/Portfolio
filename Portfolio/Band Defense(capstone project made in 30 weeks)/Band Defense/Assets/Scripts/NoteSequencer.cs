using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class NoteSequencer : MonoBehaviour
{
	// Public variables
    [SerializeField]
    private TextAsset noteData;
    public double bpm;
    public int timeSigTop = 4; // Top of the time signature: the number of beats per measure
    [SerializeField]
    private int timeSigBottom = 4; // Bottom of the time signature: the note which gets one beat
    [SerializeField]
    private int ticksPerBeat = 2;
    [SerializeField]
    private RhythmBar[] icons;

	// Internal variables
    private double nextTick = 0.0F;
    private double sampleRate = 0.0F;
    private int accent;
    private int tick = 1;
    private int measure = 0;
    private int eventIndex = 0;
    private bool running = false;
    private double samplesPerBeat;
    private string[] tickEvents;
    private string[] nextEvent;
    private string[] cue;
	private int[] lanes;

    // Not-JavaScript, Y U no swizzle?!
    private const int GUITAR = 0;
    private const int BASS = 1;
    private const int DRUMS = 2;
    private const int BACKUP = 3;
    private const int KEYS = 4;

    void Start()
    {
        lanes = LoadLanes();
        tickEvents = noteData.text.Split(';');
        accent = timeSigTop;
        nextEvent = tickEvents[eventIndex].Split(':');
        cue = nextEvent[0].Split(',');
        double startTick = AudioSettings.dspTime;
        sampleRate = AudioSettings.outputSampleRate;
        nextTick = startTick * sampleRate;
        samplesPerBeat = sampleRate * (60.0F / bpm) * 4.0f / timeSigBottom;
        running = true;
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        if (!running)
            return;

        double samplesPerTick = samplesPerBeat / ticksPerBeat; // We check for a cue spawn event once every tick
        double sample = AudioSettings.dspTime * sampleRate; // Time in the song with the precision of the sample rate (~ 1000x framerate)
        int dataLen = data.Length / channels;
        int n = 0;
        while (n < dataLen)
        {
            while (sample + n >= nextTick)
            {
                // On every tick:
                nextTick += samplesPerTick;
                if(++tick > ticksPerBeat)
                {
                    // On every beat:
                    tick = 1;
                    if (++accent > timeSigTop)
                    {
                        // On every measure:
                        accent = 1;
                        measure++;
                    }
                }

                // Spawn attack buttons based on values in the text file
                if((measure == int.Parse(cue[0])) && (accent == int.Parse(cue[1])) && (tick == int.Parse(cue[2])))
                {
                    //Spawn the attack buttons for this cue
                    string[] buttons = nextEvent[1].Split(',');
                    for(int i=0; i<buttons.Length; i++)
                    {
                        switch (buttons[i].Trim())
                        {
                            case "g":
                                //Spawn a button for the guitarist
                                Debug.Log("Guitar Note");
                                icons[lanes[GUITAR]].cue = true;
                                break;
                            case "b":
                                //Spawn a button for the bassist
                                Debug.Log("Bass Note");
                                if (lanes.Length >= 2)
                                {
                                    icons[lanes[BASS]].cue = true;
                                }
                                break;
                            case "d":
                                //Spawn a button for the drummer
                                Debug.Log("Drum Note");
                                if (lanes.Length >= 3)
                                {
                                    icons[lanes[DRUMS]].cue = true;
                                }
                                break;
                            case "v":
                                //Spawn a button for the backup vocalist
                               
                                    Debug.Log("Backup Vox Note");
                                if (lanes.Length >= 4)
                                {
                                    icons[lanes[BACKUP]].cue = true;
                                }
                                break;
                            case "k":
                                //Spawn a button for the keyboardist
                                Debug.Log("Key Note");
                                if (lanes.Length >= 5)
                                {
                                    icons[lanes[KEYS]].cue = true;
                                }
                                break;
                                /*
                            case "V":
                                //Spawn a button for the vocalist?
                                Debug.Log("Vox Note");
                                break;
                                */
                            default:
                                Debug.Log("Rhythm Bar Invalid Input: " + buttons[i].Trim());
                                break;
                        }
                    }
                    //Get the next cue
                    if(eventIndex > (tickEvents.Length - 3))
                    {
                        //Loop the cue with the track
                        eventIndex = -1;
                        measure = 0;
                    }
                    nextEvent = tickEvents[++eventIndex].Split(':');
                    cue = nextEvent[0].Split(',');
                }
            }
            n++;
        }
    }

	int[] LoadLanes()
    {
        System.IO.StreamReader data = new System.IO.StreamReader(@"lanes.txt");
        string dataToLoad = data.ReadLine();
        string[] input = dataToLoad.Split(',');
        int[] result = { -1, -1, -1, -1, -1 };
        for(int i=0; i<input.Length; i++)
        {
            if (!(input[i].Equals("")))
            {
                result[i] = int.Parse(input[i]);
            }
        }
        data.Close();
        return result;
    }
}