using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessing : MonoBehaviour
{
    [SerializeField] PostProcessVolume volume;

    ChromaticAberration chromaticAberration;
    LensDistortion lensDistortion;

    [Header("ChromaticAberration")]
    [SerializeField] float maxAberration = 0.33f;
    [SerializeField] float aberrationRiseStep = 0.005f;
    [SerializeField] float aberrationRestoreStep = 0.1f;

    [Header("LensDistortion")]
    [SerializeField] float maxDistortion = -80f;
    [SerializeField] float distortionRiseStep = 1.21f;
    [SerializeField] float distortionRestoreStep = 2.42f;

    public static PostProcessing Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        volume.profile.TryGetSettings(out chromaticAberration);
        volume.profile.TryGetSettings(out lensDistortion);

        chromaticAberration.active = true;
        lensDistortion.active = true;

        chromaticAberration.intensity.value = 0f;
        lensDistortion.intensity.value = 0f;
    }

    public void Boost()
    {
        StopAllCoroutines();
        RiseChromaticAberration();
        RiseLensDistortion();
    }

    public void EndBoost()
    {
        SetZeroChromaticAberration();
        SetZeroLensDistortion();
    }


    //ChromaticAberration

    void RiseChromaticAberration()
    {
        if (chromaticAberration.intensity.value < maxAberration)
        {
            chromaticAberration.intensity.value += aberrationRiseStep;
        }
    }

    void SetZeroChromaticAberration()
    {
        StartCoroutine(RestoreChromaticAberration());
    }


    IEnumerator RestoreChromaticAberration()
    {
        while (chromaticAberration.intensity.value > 0)
        {
            chromaticAberration.intensity.value -= aberrationRestoreStep;
            yield return new WaitForEndOfFrame();
        }
    }

    //LensDistortion

    void RiseLensDistortion()
    {
        if (lensDistortion.intensity.value > maxDistortion)
        {
            lensDistortion.intensity.value += distortionRiseStep;
        }
    }

    void SetZeroLensDistortion()
    {
        StartCoroutine(RestoreLensDistortion());
    }

    IEnumerator RestoreLensDistortion()
    {
        while (lensDistortion.intensity.value < 0)
        {
            lensDistortion.intensity.value -= distortionRestoreStep;
            yield return new WaitForEndOfFrame();
        }
        lensDistortion.intensity.value = 0f;
    }
}
