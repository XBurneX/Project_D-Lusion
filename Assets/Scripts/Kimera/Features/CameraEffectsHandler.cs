using Cinemachine.PostFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Features
{
    public class CameraEffectsHandler :  MonoBehaviour, IActivable, IFeatureSetup, IFeatureUpdate //Other channels
    {
        //Configuration
        [Header("Settings")]
        public Settings settings;
        //Control
        [Header("Control")]
        [SerializeField] private bool active;
        //States
        //Properties
        public float maxChromaIntensity;
        public float maxVignetteIntensity;
        public float oscilationSpeed;
        float furryValue;
        float chromaValue;
        float vignetteValue;
        //References
        //Componentes
        public CinemachineVolumeSettings volume;
        private ChromaticAberration chroma;
        private Vignette vig;

        public void SetupFeature(Controller controller)
        {
            settings = controller.settings;            

            volume.m_Profile.TryGet(out chroma);
            volume.m_Profile.TryGet(out vig);
            //Setup Properties
            maxChromaIntensity = settings.Search("maxChromaIntensity");
            maxVignetteIntensity = settings.Search("maxVignetteIntensity");
            oscilationSpeed = settings.Search("oscilationSpeed");

            ToggleActive(true);
        }

        public void UpdateFeature(Controller controller)
        {
            FurryEntity furry = controller as FurryEntity;

            if (furry != null) 
            {
                furryValue = furry.furryCount;
                float furryRatio = Mathf.InverseLerp(0, furry.maxFurryCount, furryValue);
                
                chroma.intensity.value = Mathf.Lerp(0, maxChromaIntensity, furryRatio);

                if (furryValue >= 40)
                {
                    
                }

                vig.intensity.value = Mathf.Lerp(0, maxVignetteIntensity, furryRatio);
            }
        }



        public bool GetActive()
        {
            return active;
        }

        public void ToggleActive(bool active)
        {
            this.active = active;
        }
    }
}