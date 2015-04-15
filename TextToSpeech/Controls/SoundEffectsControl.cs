using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectSound;
using Buffer = Microsoft.DirectX.DirectSound.SecondaryBuffer;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using JocysCom.TextToSpeech.Monitor.Audio;

namespace JocysCom.TextToSpeech.Monitor.Controls
{
    /// <summary>
    /// Summary description for Sound Effects Control.
    /// </summary>
    public partial class SoundEffectsControl : System.Windows.Forms.UserControl
    {

        public SoundEffectsControl()
        {
            // Required for Windows Form Designer support
            InitializeComponent();
            if (IsDesignMode) return;
            CurrentPreset = null;
            var effects = Enum.GetValues(typeof(EffectType)).Cast<EffectType>().Where(x => x != EffectType.None);
            SetDefaultValues();
            InitializeDevice();
        }

        public bool IsDesignMode
        {
            get { return DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime; }
        }

        private SecondaryBuffer ApplicationBuffer = null;
        private Device ApplicationDevice = null;

        private void SetDefaultValues()
        {
            ResetGeneral();
            ResetChorus();
            ResetCompressor();
            ResetDistortion();
            ResetEcho();
            ResetFlanger();
            ResetGargle();
            ResetParamEq();
            ResetReverb();
            ResetReverb3d();
        }

        void InitializeDevice()
        {
            // Create and setup the sound device.
            ApplicationDevice = new Device();
            ApplicationDevice.SetCooperativeLevel(this, CooperativeLevel.Normal);
        }

        public void LoadSoundFile(Stream stream, int sampleRate, int bitsPerSample, int channelCount)
        {
            stream.Position = 0;
            // Create and setup the buffer description.
            BufferDescription buffer_desc = new BufferDescription();
            buffer_desc.Format = new WaveFormat
            {
                BitsPerSample = (short)bitsPerSample,
                SamplesPerSecond = sampleRate,
                Channels = (short)channelCount,
            };
            // This has to be true to use effects.
            buffer_desc.ControlEffects = true;
            // Play sound even if application loses focus.
            buffer_desc.GlobalFocus = true;
            // Create and setup the buffer for playing the sound.
            ApplicationBuffer = new SecondaryBuffer(stream, buffer_desc, ApplicationDevice);
        }

        public void LoadSoundFile(byte[] bytes, int sampleRate, int bitsPerSample, int channelCount)
        {
            var ms = new MemoryStream(bytes);
            ms.Position = 0;
            LoadSoundFile(ms, sampleRate, bitsPerSample, channelCount);
        }

        public int EffectsCount
        {
            get
            {
                var boxes = new CheckBox[] {
                    ChorusCheckBox,
                    CompressorCheckBox,
                    DistortionCheckBox,
                    EchoCheckBox,
                    FlangerCheckBox,
                    GargleCheckBox,
                    ParamEqCheckBox,
                    ReverbCheckBox,
                    Reverb3dCheckBox,
                };
                var effectsCount = boxes.Count(x => x.Checked);
                return effectsCount;
            }
        }

        public void StopSound()
        {
            // Build the effects array
            //ApplicationBuffer.Volume = -10000;
            var ab = ApplicationBuffer;
            if (ab != null)
            {
                ab.Stop();
            }
            //ApplicationBuffer.Volume = 0;
        }

        public void PlaySound()
        {

            if (ApplicationBuffer != null)
            {
                if (EffectsCount > 0)
                {
                    int arrayCount = 0;
                    EffectDescription[] effects = new EffectDescription[EffectsCount];
                    if (ChorusCheckBox.Checked)
                    {
                        effects[arrayCount].GuidEffectClass = DSoundHelper.StandardChorusGuid;
                        arrayCount += 1;
                    }
                    if (CompressorCheckBox.Checked)
                    {
                        effects[arrayCount].GuidEffectClass = DSoundHelper.StandardCompressorGuid;
                        arrayCount += 1;
                    }
                    if (DistortionCheckBox.Checked)
                    {
                        effects[arrayCount].GuidEffectClass = DSoundHelper.StandardDistortionGuid;
                        arrayCount += 1;
                    }
                    if (EchoCheckBox.Checked)
                    {
                        effects[arrayCount].GuidEffectClass = DSoundHelper.StandardEchoGuid;
                        arrayCount += 1;
                    }
                    if (FlangerCheckBox.Checked)
                    {
                        effects[arrayCount].GuidEffectClass = DSoundHelper.StandardFlangerGuid;
                        arrayCount += 1;
                    }
                    if (GargleCheckBox.Checked)
                    {
                        effects[arrayCount].GuidEffectClass = DSoundHelper.StandardGargleGuid;
                        arrayCount += 1;
                    }
                    if (ParamEqCheckBox.Checked)
                    {
                        effects[arrayCount].GuidEffectClass = DSoundHelper.StandardParamEqGuid;
                        arrayCount += 1;
                    }
                    if (ReverbCheckBox.Checked)
                    {
                        effects[arrayCount].GuidEffectClass = DSoundHelper.StandardWavesReverbGuid;
                        arrayCount += 1;
                    }
                    if (Reverb3dCheckBox.Checked)
                    {
                        effects[arrayCount].GuidEffectClass = DSoundHelper.StandardInteractive3DLevel2ReverbGuid;
                        arrayCount += 1;
                    }
                    ApplicationBuffer.SetEffects(effects);
                    arrayCount = 0;
                    if (ChorusCheckBox.Checked)
                    {
                        ChorusEffect chorus = (ChorusEffect)ApplicationBuffer.GetEffects(arrayCount);
                        EffectsChorus chorusParamaters = chorus.AllParameters;
                        ApplyChorusEffect(ref chorusParamaters);
                        chorus.AllParameters = chorusParamaters;
                        arrayCount += 1;
                    }
                    if (CompressorCheckBox.Checked)
                    {
                        CompressorEffect compressor = (CompressorEffect)ApplicationBuffer.GetEffects(arrayCount);
                        EffectsCompressor compressorParamaters = compressor.AllParameters;
                        ApplyCompressorEffect(ref compressorParamaters);
                        compressor.AllParameters = compressorParamaters;
                        arrayCount += 1;
                    }
                    if (DistortionCheckBox.Checked)
                    {
                        DistortionEffect distortion = (DistortionEffect)ApplicationBuffer.GetEffects(arrayCount);
                        EffectsDistortion distortionParamaters = distortion.AllParameters;
                        ApplyDistortionEffect(ref distortionParamaters);
                        distortion.AllParameters = distortionParamaters;
                        arrayCount += 1;
                    }
                    if (EchoCheckBox.Checked)
                    {
                        EchoEffect echo = (EchoEffect)ApplicationBuffer.GetEffects(arrayCount);
                        EffectsEcho echoParamaters = echo.AllParameters;
                        ApplyEchoEffect(ref echoParamaters);
                        echo.AllParameters = echoParamaters;
                        arrayCount += 1;
                    }
                    if (FlangerCheckBox.Checked)
                    {
                        FlangerEffect flanger = (FlangerEffect)ApplicationBuffer.GetEffects(arrayCount);
                        EffectsFlanger flangerParamaters = flanger.AllParameters;
                        ApplyFlangerEffect(ref flangerParamaters);
                        flanger.AllParameters = flangerParamaters;
                        arrayCount += 1;
                    }
                    if (GargleCheckBox.Checked)
                    {
                        GargleEffect gargle = (GargleEffect)ApplicationBuffer.GetEffects(arrayCount);
                        EffectsGargle gargleParamaters = gargle.AllParameters;
                        ApplyGargleEffect(ref gargleParamaters);
                        gargle.AllParameters = gargleParamaters;
                        arrayCount += 1;
                    }
                    if (ParamEqCheckBox.Checked)
                    {
                        ParamEqEffect paramEq = (ParamEqEffect)ApplicationBuffer.GetEffects(arrayCount);
                        EffectsParamEq paramEqParamaters = paramEq.AllParameters;
                        ApplyParamEqEffect(ref paramEqParamaters);
                        paramEq.AllParameters = paramEqParamaters;
                        arrayCount += 1;
                    }
                    if (ReverbCheckBox.Checked)
                    {
                        WavesReverbEffect reverb = (WavesReverbEffect)ApplicationBuffer.GetEffects(arrayCount);
                        EffectsWavesReverb reverbParamaters = reverb.AllParameters;
                        ApplyReverbEffect(ref reverbParamaters);
                        reverb.AllParameters = reverbParamaters;
                        arrayCount += 1;
                    }
                    if (Reverb3dCheckBox.Checked)
                    {
                        Interactive3DLevel2ReverbEffect reverb3D = (Interactive3DLevel2ReverbEffect)ApplicationBuffer.GetEffects(arrayCount);
                        EffectsInteractive3DLevel2Reverb reverb3DParamaters = reverb3D.AllParameters;
                        ApplyReverb3dEffect(ref reverb3DParamaters);
                        reverb3D.AllParameters = reverb3DParamaters;
                        arrayCount += 1;
                    }

                }
            }
            if (null != ApplicationBuffer)
            {
                ApplicationBuffer.Play(0, BufferPlayFlags.Default);
            }
        }

        private void mnuStop_Click(object sender, System.EventArgs e)
        {
            if (null != ApplicationBuffer && ApplicationBuffer.PlayPosition != 0)
            {
                ApplicationBuffer.Volume = -10000;
                ApplicationBuffer.Stop();
                ApplicationBuffer.SetCurrentPosition(0);
                ApplicationBuffer.Volume = 0;
            }
        }

        private void EffectCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var cbx = (CheckBox)sender;
            var name = cbx.Name.Replace("CheckBox", "") + "TabPage";
            var tab = EffectsTabControl.TabPages.Cast<TabPage>().FirstOrDefault(x => x.Name == name);
            tab.ImageIndex = cbx.Checked ? 1 : 0;
        }

        Dictionary<TrackBar, decimal> multipliers = new Dictionary<TrackBar, decimal>();

        void SetTrackBar(TrackBar control, float value)
        {
            var multiplier = multipliers[control];
            var v = (int)((decimal)value * multiplier);
            if (v < control.Minimum) control.Value = control.Minimum;
            else if (v > control.Maximum) control.Value = control.Maximum;
            else control.Value = v;
        }

        void ResetTrackBar(TrackBar control, float min, float max, float defaultValue, decimal multiplier = 1.0m)
        {
            if (multipliers.ContainsKey(control)) multipliers[control] = multiplier;
            else multipliers.Add(control, multiplier);
            control.Minimum = (int)((decimal)min * multiplier);
            control.Maximum = (int)((decimal)max * multiplier);
            control.TickFrequency = (Math.Abs(control.Maximum - control.Minimum) < 20)
                ? 1
                : (int)((decimal)(max - min) * multiplier / 20);
            int value = (int)((decimal)defaultValue * multiplier);
            // Make sure value event triggers.
            if (value == control.Value)
            {
                control.Value = control.Minimum;
                control.Value = control.Maximum;
            }
            control.Value = value;
        }

        // http://msdn.microsoft.com/en-us/library/windows/desktop/ee780227%28v=vs.85%29.aspx

        #region Effect: General

        void ResetGeneral()
        {
            ResetTrackBar(GeneralPitchTrackBar, 0.5F, 2.0F, 1.0F, 100m);
        }

        private void GeneralResetButton_Click(object sender, EventArgs e)
        {
            ResetGeneral();
        }

        private void GeneralPitchTrackBar_ValueChanged(object sender, EventArgs e)
        {
            // Convert from ±100 to [0.5-2.0]
            //var pitchShift = Math.Round(Math.Pow(Math.Pow(2F, 1F / 100F), (float)GeneralPitchTrackBar.Value), 2);
            GeneralPitchValueLabel.Text = ((float)GeneralPitchTrackBar.Value / 100F).ToString("#0%");
        }

        public void ApplyGeneralEffect(ref EffectsGeneral parameters)
        {
            //var pitchShift = (float)Math.Pow(Math.Pow(2F, 1F / 100F), (float)GeneralPitchTrackBar.Value);
            parameters.Pitch = ((float)GeneralPitchTrackBar.Value / 100F);
        }

        public void LoadGeneralEffect(EffectsGeneral parameters)
        {
            SetTrackBar(GeneralPitchTrackBar, parameters.Pitch);
        }

        #endregion

        #region Effect: Chorus

        Dictionary<RadioButton, int> ChorusPhases = new Dictionary<RadioButton, int>();
        Dictionary<RadioButton, int> ChorusWaveforms = new Dictionary<RadioButton, int>();

        void ResetChorus()
        {
            ChorusPhases.Clear();
            ChorusPhases.Add(ChorusPhaseNegative180RadioButton, ChorusEffect.PhaseNegative180);
            ChorusPhases.Add(ChorusPhaseNegative90RadioButton, ChorusEffect.PhaseNegative90);
            ChorusPhases.Add(ChorusPhaseZeroRadioButton, ChorusEffect.PhaseZero);
            ChorusPhases.Add(ChorusPhase90RadioButton, ChorusEffect.Phase90);
            ChorusPhases.Add(ChorusPhase180RadioButton, ChorusEffect.Phase180);
            ChorusWaveforms.Clear();
            ChorusWaveforms.Add(ChorusWaveSinRadioButton, ChorusEffect.WaveSin);
            ChorusWaveforms.Add(ChorusWaveTriangleRadioButton, ChorusEffect.WaveTriangle);
            ResetTrackBar(ChorusDelayTrackBar, ChorusEffect.DelayMin, ChorusEffect.DelayMax, 16F, 10m);
            ResetTrackBar(ChorusDepthTrackBar, ChorusEffect.DepthMin, ChorusEffect.DepthMax, 10F);
            ResetTrackBar(ChorusFeedbackTrackBar, ChorusEffect.FeedbackMin, ChorusEffect.FeedbackMax, 25F);
            ResetTrackBar(ChorusFrequencyTrackBar, ChorusEffect.FrequencyMin, ChorusEffect.FrequencyMax, 1.1F, 10m);
            ResetTrackBar(ChorusWetDryMixTrackBar, ChorusEffect.WetDryMixMin, ChorusEffect.WetDryMixMax, 50F);
            ChorusPhase90RadioButton.Checked = true;
            ChorusWaveSinRadioButton.Checked = true;
        }

        void ApplyChorusEffect(ref EffectsChorus parameters)
        {
            parameters.Delay = (float)ChorusDelayTrackBar.Value / 10.0F;
            parameters.Depth = ChorusDepthTrackBar.Value;
            parameters.Feedback = ChorusFeedbackTrackBar.Value;
            parameters.Frequency = ChorusFrequencyTrackBar.Value / 10.0F;
            parameters.WetDryMix = ChorusWetDryMixTrackBar.Value;
            parameters.Phase = ChorusPhases.FirstOrDefault(x => x.Key.Checked).Value;
            parameters.Waveform = ChorusWaveforms.FirstOrDefault(x => x.Key.Checked).Value;
        }

        void LoadChorusEffect(EffectsChorus parameters)
        {
            SetTrackBar(ChorusDelayTrackBar, parameters.Delay);
            SetTrackBar(ChorusDepthTrackBar, parameters.Depth);
            SetTrackBar(ChorusFeedbackTrackBar, parameters.Feedback);
            SetTrackBar(ChorusFrequencyTrackBar, parameters.Frequency);
            SetTrackBar(ChorusWetDryMixTrackBar, parameters.WetDryMix);
            var phase = parameters.Phase;
            var rb = ChorusPhases.FirstOrDefault(x => x.Value == phase).Key.Checked = true;
            var waveform = parameters.Waveform;
            rb = ChorusWaveforms.FirstOrDefault(x => x.Value == waveform).Key.Checked = true;
        }

        private void ChorusResetButton_Click(object sender, EventArgs e)
        {
            ResetChorus();
        }

        private void ChorusDelayTrackBar_ValueChnaged(object sender, System.EventArgs e)
        {
            ChorusDelayValueLabel.Text = ((float)ChorusDelayTrackBar.Value / 10.0F).ToString() + " ms";
        }

        private void ChorusDepthTrackBar_ValueChnaged(object sender, System.EventArgs e)
        {
            ChorusDepthValueLabel.Text = ChorusDepthTrackBar.Value.ToString() + " %";
        }

        private void ChorusFeedbackTrackBar_ValueChnaged(object sender, System.EventArgs e)
        {
            ChorusFeedbackValueLabel.Text = ChorusFeedbackTrackBar.Value.ToString() + " %";
        }

        private void ChorusFrequencyTrackBar_ValueChnaged(object sender, System.EventArgs e)
        {
            ChorusFrequencyValueLabel.Text = ((float)ChorusFrequencyTrackBar.Value / 10.0F).ToString();
        }

        private void ChorusWetDryMixTrackBar_ValueChnaged(object sender, System.EventArgs e)
        {
            ChorusWetDryMixValueLabel.Text = ChorusWetDryMixTrackBar.Value.ToString() + " : 1";
        }

        #endregion

        #region Effect: Compressor

        void ResetCompressor()
        {
            ResetTrackBar(CompressorAttackTrackBar, CompressorEffect.AttackMin, CompressorEffect.AttackMax, 10F);
            ResetTrackBar(CompressorGainTrackBar, CompressorEffect.GainMin, CompressorEffect.GainMax, 0F);
            ResetTrackBar(CompressorPreDelayTrackBar, CompressorEffect.PreDelayMin, CompressorEffect.PreDelayMax, 4F);
            ResetTrackBar(CompressorRatioTrackBar, CompressorEffect.RatioMin, CompressorEffect.RatioMax, 3F);
            ResetTrackBar(CompressorReleaseTrackBar, CompressorEffect.ReleaseMin, CompressorEffect.ReleaseMax, 200F);
            ResetTrackBar(CompressorThresholdTrackBar, CompressorEffect.ThresholdMin, CompressorEffect.ThresholdMax, -20F);
        }

        void ApplyCompressorEffect(ref EffectsCompressor parameters)
        {
            parameters.Attack = CompressorAttackTrackBar.Value;
            parameters.Gain = CompressorGainTrackBar.Value;
            parameters.Predelay = CompressorPreDelayTrackBar.Value;
            parameters.Ratio = CompressorRatioTrackBar.Value;
            parameters.Release = CompressorReleaseTrackBar.Value;
            parameters.Threshold = CompressorThresholdTrackBar.Value;
        }

        void LoadCompressorEffect(EffectsCompressor parameters)
        {
            SetTrackBar(CompressorAttackTrackBar, parameters.Attack);
            SetTrackBar(CompressorGainTrackBar, parameters.Gain);
            SetTrackBar(CompressorPreDelayTrackBar, parameters.Predelay);
            SetTrackBar(CompressorRatioTrackBar, parameters.Ratio);
            SetTrackBar(CompressorReleaseTrackBar, parameters.Release);
            SetTrackBar(CompressorThresholdTrackBar, parameters.Threshold);
        }

        private void CompressorResetButton_Click(object sender, EventArgs e)
        {
            ResetCompressor();
        }

        private void CompressorAttackTrackBar_ValueChanged(object sender, System.EventArgs e)
        {
            CompressorAttackValueLabel.Text = CompressorAttackTrackBar.Value.ToString() + " ms";
        }

        private void CompressorGainTrackBar_ValueChanged(object sender, System.EventArgs e)
        {
            CompressorGainValueLabel.Text = CompressorGainTrackBar.Value.ToString() + " dB";
        }

        private void CompressorPredelayTrackBar_ValueChanged(object sender, System.EventArgs e)
        {
            CompressorPredelayValueLabel.Text = CompressorPreDelayTrackBar.Value.ToString() + " ms";
        }

        private void CompressorRatioTrackBar_ValueChanged(object sender, System.EventArgs e)
        {
            CompressorRatioValueLabel.Text = CompressorRatioTrackBar.Value.ToString() + " : 1";
        }

        private void CompressorReleaseTrackBar_ValueChanged(object sender, System.EventArgs e)
        {
            CompressorReleaseValueLabel.Text = CompressorReleaseTrackBar.Value.ToString() + " ms";
        }

        private void CompressorThresholdTrackBar_ValueChanged(object sender, System.EventArgs e)
        {
            CompressorThresholdValueLabel.Text = CompressorThresholdTrackBar.Value.ToString() + " dB";
        }


        #endregion

        #region Effect: Distortion

        void ResetDistortion()
        {
            ResetTrackBar(DistortionEdgeTrackBar, DistortionEffect.EdgeMin, DistortionEffect.EdgeMax, 15F);
            ResetTrackBar(DistortionGainTrackBar, DistortionEffect.GainMin, DistortionEffect.GainMax, -18F);
            ResetTrackBar(DistortionBandwidthTrackBar, DistortionEffect.PostEqBandwidthMin, DistortionEffect.PostEqBandwidthMax, 2400F);
            ResetTrackBar(DistortionFrequencyTrackBar, DistortionEffect.PostEqCenterFrequencyMin, DistortionEffect.PostEqCenterFrequencyMax, 2400F);
            ResetTrackBar(DistortionLowPassTrackBar, DistortionEffect.PreLowPassCutoffMin, DistortionEffect.PreLowPassCutoffMax, 8000F);
        }

        void ApplyDistortionEffect(ref EffectsDistortion parameters)
        {
            parameters.Edge = DistortionEdgeTrackBar.Value;
            parameters.Gain = DistortionGainTrackBar.Value;
            parameters.PostEqBandwidth = DistortionBandwidthTrackBar.Value;
            parameters.PostEqCenterFrequency = DistortionFrequencyTrackBar.Value;
            parameters.PreLowpassCutoff = DistortionLowPassTrackBar.Value;
        }

        void LoadDistortionEffect(EffectsDistortion parameters)
        {
            SetTrackBar(DistortionEdgeTrackBar, parameters.Edge);
            SetTrackBar(DistortionGainTrackBar, parameters.Gain);
            SetTrackBar(DistortionBandwidthTrackBar, parameters.PostEqBandwidth);
            SetTrackBar(DistortionFrequencyTrackBar, parameters.PostEqCenterFrequency);
            SetTrackBar(DistortionLowPassTrackBar, parameters.PreLowpassCutoff);
        }

        private void DistortionResetButton_Click(object sender, EventArgs e)
        {
            ResetDistortion();
        }

        private void DistortionEdgeTrackBar_ValueChanged(object sender, System.EventArgs e)
        {
            this.DistortionEdgeValueLabel.Text = DistortionEdgeTrackBar.Value.ToString() + " %";
        }

        private void DistortionGainTrackBar_ValueChanged(object sender, System.EventArgs e)
        {
            this.DistortionGainValueLabel.Text = DistortionGainTrackBar.Value.ToString() + " dB";
        }

        private void DistortionQTrackBar_ValueChanged(object sender, System.EventArgs e)
        {
            this.DistortionBandwidthValueLabel.Text = DistortionBandwidthTrackBar.Value.ToString() + " Hz";
        }

        private void DistortionFrequencyTrackBar_ValueChanged(object sender, System.EventArgs e)
        {
            this.DistortionFrequencyValueLabel.Text = DistortionFrequencyTrackBar.Value.ToString() + " Hz";
        }

        private void DistortionLowPassTrackBar_ValueChanged(object sender, System.EventArgs e)
        {
            this.DistortionLowpassValueLabel.Text = DistortionLowPassTrackBar.Value.ToString() + " Hz";
        }


        #endregion

        #region Effect: Echo

        void ResetEcho()
        {
            ResetTrackBar(EchoFeedbackTrackBar, EchoEffect.FeedbackMin, EchoEffect.FeedbackMax, 50F);
            ResetTrackBar(EchoLeftDelayTrackBar, EchoEffect.LeftDelayMin, EchoEffect.LeftDelayMax, 100F);
            ResetTrackBar(EchoRightDelayTrackBar, EchoEffect.RightDelayMin, EchoEffect.RightDelayMax, 300F);
            ResetTrackBar(EchoWetDryMixTrackBar, EchoEffect.WetDryMixMin, EchoEffect.WetDryMixMax, 50F);
            EchoPanDelayCheckBox.Checked = false;
        }

        void ApplyEchoEffect(ref EffectsEcho parameters)
        {
            parameters.Feedback = EchoFeedbackTrackBar.Value;
            parameters.LeftDelay = EchoLeftDelayTrackBar.Value;
            parameters.PanDelay = EchoPanDelayCheckBox.Checked ? EchoEffect.PanDelayMax : EchoEffect.PanDelayMin;
            parameters.LeftDelay = EchoRightDelayTrackBar.Value;
            parameters.WetDryMix = EchoWetDryMixTrackBar.Value;
        }

        void LoadEchoEffect(EffectsEcho parameters)
        {
            SetTrackBar(EchoFeedbackTrackBar, parameters.Feedback);
            SetTrackBar(EchoLeftDelayTrackBar, parameters.LeftDelay);
            SetTrackBar(EchoRightDelayTrackBar, parameters.RightDelay);
            SetTrackBar(EchoWetDryMixTrackBar, parameters.WetDryMix);
            EchoPanDelayCheckBox.Checked = (parameters.PanDelay == EchoEffect.PanDelayMax);
        }

        private void EchoResetButton_Click(object sender, EventArgs e)
        {
            ResetEcho();
        }

        private void EchoFeedbackTrackBar_ValueChanged(object sender, System.EventArgs e)
        {
            this.EchoFeedBackValueLabel.Text = EchoFeedbackTrackBar.Value.ToString() + " %";
        }

        private void EchoLeftDelayTrackBar_ValueChanged(object sender, System.EventArgs e)
        {
            this.EchoLeftDelayValueLabel.Text = EchoLeftDelayTrackBar.Value.ToString() + " ms";
        }

        private void EchoRightDelayTrackBar_ValueChanged(object sender, System.EventArgs e)
        {
            this.EchoRightDelayValueLabel.Text = EchoRightDelayTrackBar.Value.ToString() + " ms";
        }

        private void EchoWetDryMixTrackBar_ValueChanged(object sender, System.EventArgs e)
        {
            this.EchoWetDryMixValueLabel.Text = EchoWetDryMixTrackBar.Value.ToString() + " : 1";
        }


        #endregion

        #region Effect: Flanger

        Dictionary<RadioButton, int> FlangerPhases = new Dictionary<RadioButton, int>();
        Dictionary<RadioButton, int> FlangerWaveforms = new Dictionary<RadioButton, int>();

        void ResetFlanger()
        {
            FlangerPhases.Clear();
            FlangerPhases.Add(FlangerPhaseNegative180RadioButton, FlangerEffect.PhaseNeg180);
            FlangerPhases.Add(FlangerPhaseNegative90RadioButton, FlangerEffect.PhaseNeg90);
            FlangerPhases.Add(FlangerPhaseZeroRadioButton, FlangerEffect.PhaseZero);
            FlangerPhases.Add(FlangerPhase90RadioButton, FlangerEffect.Phase90);
            FlangerPhases.Add(FlangerPhase180RadioButton, FlangerEffect.Phase180);
            FlangerWaveforms.Clear();
            FlangerWaveforms.Add(FlangerWaveSinRadioButton, FlangerEffect.WaveSin);
            FlangerWaveforms.Add(FlangerWaveTriangleRadioButton, FlangerEffect.WaveTriangle);
            ResetTrackBar(FlangerDelayTrackBar, FlangerEffect.DelayMin, FlangerEffect.DelayMax, 2F, 10m);
            ResetTrackBar(FlangerDepthTrackBar, FlangerEffect.DepthMin, FlangerEffect.DepthMax, 100F);
            ResetTrackBar(FlangerFeedbackTrackBar, FlangerEffect.FeedbackMin, FlangerEffect.FeedbackMax, -50F);
            ResetTrackBar(FlangerFrequencyTrackBar, FlangerEffect.FrequencyMin, FlangerEffect.FrequencyMax, 0F, 10m);
            ResetTrackBar(FlangerWetDryMixTrackBar, FlangerEffect.WetDryMixMin, FlangerEffect.WetDryMixMax, 50F);
            FlangerPhase90RadioButton.Checked = true;
            FlangerWaveSinRadioButton.Checked = true;
        }

        void ApplyFlangerEffect(ref EffectsFlanger parameters)
        {
            parameters.Delay = (float)FlangerDelayTrackBar.Value / 10.0F;
            parameters.Depth = FlangerDepthTrackBar.Value;
            parameters.Feedback = FlangerFeedbackTrackBar.Value;
            parameters.Frequency = (float)FlangerFrequencyTrackBar.Value / 10.0F;
            parameters.Phase = FlangerPhases.FirstOrDefault(x => x.Key.Checked).Value;
            parameters.WetDryMix = FlangerWetDryMixTrackBar.Value;
            parameters.Waveform = FlangerWaveforms.FirstOrDefault(x => x.Key.Checked).Value;
        }

        void LoadFlangerEffect(EffectsFlanger parameters)
        {
            SetTrackBar(FlangerDelayTrackBar, parameters.Delay);
            SetTrackBar(FlangerDepthTrackBar, parameters.Depth);
            SetTrackBar(FlangerFeedbackTrackBar, parameters.Feedback);
            SetTrackBar(FlangerFrequencyTrackBar, parameters.Frequency);
            SetTrackBar(FlangerWetDryMixTrackBar, parameters.WetDryMix);
            var phase = parameters.Phase;
            var rb = FlangerPhases.FirstOrDefault(x => x.Value == phase).Key.Checked = true;
            var waveform = parameters.Waveform;
            rb = FlangerWaveforms.FirstOrDefault(x => x.Value == waveform).Key.Checked = true;
        }

        private void FlangerResetButton_Click(object sender, EventArgs e)
        {
            ResetFlanger();
        }

        private void FlangerDelayTrackBar_ValueChnaged(object sender, System.EventArgs e)
        {
            FlangerDelayValueLabel.Text = ((float)FlangerDelayTrackBar.Value / 10.0F).ToString() + " ms";
        }

        private void FlangerDepthTrackBar_ValueChnaged(object sender, System.EventArgs e)
        {
            FlangerDepthValueLabel.Text = FlangerDepthTrackBar.Value.ToString() + " %";
        }

        private void FlangerFeedbackTrackBar_ValueChnaged(object sender, System.EventArgs e)
        {
            FlangerFeedbackValueLabel.Text = FlangerFeedbackTrackBar.Value.ToString() + " %";
        }

        private void FlangerFrequencyTrackBar_ValueChnaged(object sender, System.EventArgs e)
        {
            FlangerFrequencyValueLabel.Text = ((float)FlangerFrequencyTrackBar.Value / 10.0F).ToString();
        }

        private void FlangerWetDryMixTrackBar_ValueChnaged(object sender, System.EventArgs e)
        {
            FlangerWetDryMixValueLabel.Text = FlangerWetDryMixTrackBar.Value.ToString() + " : 1";
        }

        #endregion

        #region Effect: Gargle

        Dictionary<RadioButton, int> GargleWaveShapes = new Dictionary<RadioButton, int>();

        void ResetGargle()
        {
            GargleWaveShapes.Clear();
            GargleWaveShapes.Add(GargleWaveSquareRadioButton, GargleEffect.WaveSquare);
            GargleWaveShapes.Add(GargleWaveTriangleRadioButton, GargleEffect.WaveTriangle);
            ResetTrackBar(GargleRateHzTrackBar, GargleEffect.RateHzMin, GargleEffect.RateHzMax, 20F);
            GargleWaveTriangleRadioButton.Checked = true;
        }

        void ApplyGargleEffect(ref EffectsGargle parameters)
        {
            parameters.RateHz = GargleRateHzTrackBar.Value;
            parameters.WaveShape = GargleWaveShapes.FirstOrDefault(x => x.Key.Checked).Value;
        }

        void LoadGargleEffect(EffectsGargle parameters)
        {
            SetTrackBar(GargleRateHzTrackBar, parameters.RateHz);
            var shape = parameters.WaveShape;
            var rb = GargleWaveShapes.FirstOrDefault(x => x.Value == shape).Key.Checked = true;
        }

        private void GargleResetButton_Click(object sender, EventArgs e)
        {
            ResetGargle();
        }

        private void GargleRateHzTrackBar_ValueChanged(object sender, EventArgs e)
        {
            GargleRateHzValueLabel.Text = GargleRateHzTrackBar.Value + " Hz";
        }

        #endregion

        #region Effect: Param EQ

        void ResetParamEq()
        {
            ResetTrackBar(ParamEqBandwidthTrackBar, ParamEqEffect.BandwidthMin, ParamEqEffect.BandwidthMax, 12F);
            ResetTrackBar(ParamEqCenterTrackBar, ParamEqEffect.CenterMin, ParamEqEffect.CenterMax, 8000F);
            ResetTrackBar(ParamEqGainTrackBar, ParamEqEffect.GainMin, ParamEqEffect.GainMax, 0F);
        }

        void ApplyParamEqEffect(ref EffectsParamEq parameters)
        {
            parameters.Bandwidth = ParamEqBandwidthTrackBar.Value;
            parameters.Center = ParamEqCenterTrackBar.Value;
            parameters.Gain = ParamEqGainTrackBar.Value;
        }

        void LoadParamEqEffect(EffectsParamEq parameters)
        {
            SetTrackBar(ParamEqBandwidthTrackBar, parameters.Bandwidth);
            SetTrackBar(ParamEqCenterTrackBar, parameters.Center);
            SetTrackBar(ParamEqGainTrackBar, parameters.Gain);
        }

        private void ParamEqResetButton_Click(object sender, EventArgs e)
        {
            ResetParamEq();
        }

        private void ParamEqBandwidthHzTrackBar_ValueChanged(object sender, EventArgs e)
        {
            ParamEqBandwidthValueLabel.Text = ParamEqBandwidthTrackBar.Value + " st";
        }

        private void ParamEqCenterTrackBar_ValueChanged(object sender, EventArgs e)
        {
            ParamEqCenterValueLabel.Text = ParamEqCenterTrackBar.Value + " Hz";
        }

        private void ParamEqGainTrackBar_ValueChanged(object sender, EventArgs e)
        {
            ParamEqGainValueLabel.Text = ParamEqGainTrackBar.Value.ToString();
        }

        #endregion

        #region Effect: Reverb

        void ResetReverb()
        {
            ResetTrackBar(ReverbRatioTrackBar, WavesReverbEffect.HighFrequencyRtRatioMin, WavesReverbEffect.HighFrequencyRtRatioMax, WavesReverbEffect.HighFrequencyRtRatioDefault, 1000m);
            ResetTrackBar(ReverbInputGainTrackBar, WavesReverbEffect.InGainMin, WavesReverbEffect.InGainMax, WavesReverbEffect.InGainDefault);
            ResetTrackBar(ReverbMixTrackBar, WavesReverbEffect.ReverbMixMin, WavesReverbEffect.ReverbMixMax, WavesReverbEffect.ReverbMixDefault);
            ResetTrackBar(ReverbTimeTrackBar, WavesReverbEffect.ReverbTimeMin, WavesReverbEffect.ReverbTimeMax, WavesReverbEffect.ReverbTimeDefault);
        }

        void ApplyReverbEffect(ref EffectsWavesReverb parameters)
        {
            parameters.HighFrequencyRtRatio = (float)ReverbRatioTrackBar.Value / 1000.0F;
            parameters.InGain = ReverbInputGainTrackBar.Value;
            parameters.ReverbMix = ReverbMixTrackBar.Value;
            parameters.ReverbTime = ReverbTimeTrackBar.Value;
        }

        void LoadReverbEffect(EffectsWavesReverb parameters)
        {
            SetTrackBar(ReverbRatioTrackBar, parameters.HighFrequencyRtRatio);
            SetTrackBar(ReverbInputGainTrackBar, parameters.InGain);
            SetTrackBar(ReverbMixTrackBar, parameters.ReverbMix);
            SetTrackBar(ReverbTimeTrackBar, parameters.ReverbTime);
        }

        private void ReverbResetButton_Click(object sender, EventArgs e)
        {
            ResetReverb();
        }

        private void ReverbRatioTrackBar_ValueChanged(object sender, EventArgs e)
        {
            ReverbRatioValueLabel.Text = ((float)ReverbRatioTrackBar.Value / 1000.0F).ToString();
        }

        private void ReverbInputGainTrackBar_ValueChanged(object sender, EventArgs e)
        {
            ReverbInputGainValueLabel.Text = ReverbInputGainTrackBar.Value + " dB";
        }

        private void ReverbMixTrackBar_ValueChanged(object sender, EventArgs e)
        {
            ReverbMixValueLabel.Text = ReverbMixTrackBar.Value + " dB";
        }

        private void ReverbTimeTrackBar_ValueChanged(object sender, EventArgs e)
        {
            ReverbTimeValueLabel.Text = ReverbTimeTrackBar.Value.ToString() + " ms";
        }

        #endregion

        #region Effect: Reverb 3D

        void ResetReverb3d()
        {
            ResetTrackBar(Reverb3dDecayHfRatioTrackBar, Interactive3DLevel2ReverbEffect.DecayHfRatioMin, Interactive3DLevel2ReverbEffect.DecayHfRatioMax, Interactive3DLevel2ReverbEffect.DecayHfRatioDefault, 100m);
            ResetTrackBar(Reverb3dDecayTimeTrackBar, Interactive3DLevel2ReverbEffect.DecayTimeMin, Interactive3DLevel2ReverbEffect.DecayTimeMax, Interactive3DLevel2ReverbEffect.DecayTimeDefault, 100m);
            ResetTrackBar(Reverb3dDensityTrackBar, Interactive3DLevel2ReverbEffect.DensityMin, Interactive3DLevel2ReverbEffect.DensityMax, Interactive3DLevel2ReverbEffect.DensityDefault);
            ResetTrackBar(Reverb3dDiffusionTrackBar, Interactive3DLevel2ReverbEffect.DiffusionMin, Interactive3DLevel2ReverbEffect.DiffusionMax, Interactive3DLevel2ReverbEffect.DiffusionDefault);
            ResetTrackBar(Reverb3dHfReferenceTrackBar, Interactive3DLevel2ReverbEffect.HfReferenceMin, Interactive3DLevel2ReverbEffect.HfReferenceMax, Interactive3DLevel2ReverbEffect.HfReferenceDefault);
            ResetTrackBar(Reverb3dReflectionsTrackBar, Interactive3DLevel2ReverbEffect.ReflectionsMin, Interactive3DLevel2ReverbEffect.ReflectionsMax, Interactive3DLevel2ReverbEffect.ReflectionsDefault);
            ResetTrackBar(Reverb3dReflectionsDelayTrackBar, Interactive3DLevel2ReverbEffect.ReflectionsDelayMin, Interactive3DLevel2ReverbEffect.ReflectionsDelayMax, Interactive3DLevel2ReverbEffect.ReflectionsDelayDefault, 1000m);
            ResetTrackBar(Reverb3dReverbTrackBar, Interactive3DLevel2ReverbEffect.ReverbMin, Interactive3DLevel2ReverbEffect.ReverbMax, Interactive3DLevel2ReverbEffect.ReverbDefault);
            ResetTrackBar(Reverb3dReverbDelayTrackBar, Interactive3DLevel2ReverbEffect.ReverbDelayMin, Interactive3DLevel2ReverbEffect.ReverbDelayMax, Interactive3DLevel2ReverbEffect.ReverbDelayDefault, 1000m);
            ResetTrackBar(Reverb3dRoomTrackBar, Interactive3DLevel2ReverbEffect.RoomMin, Interactive3DLevel2ReverbEffect.RoomMax, Interactive3DLevel2ReverbEffect.RoomDefault);
            ResetTrackBar(Reverb3dRoomHfTrackBar, Interactive3DLevel2ReverbEffect.RoomHfMin, Interactive3DLevel2ReverbEffect.RoomHfMax, Interactive3DLevel2ReverbEffect.RoomHfDefault);
            ResetTrackBar(Reverb3dRoomRollOffFactorTrackBar, Interactive3DLevel2ReverbEffect.RoomRollOffFactorMin, Interactive3DLevel2ReverbEffect.RoomRollOffFactorMax, Interactive3DLevel2ReverbEffect.RoomRollOffFactorDefault);
        }

        void ApplyReverb3dEffect(ref EffectsInteractive3DLevel2Reverb parameters)
        {
            parameters.DecayHfRatio = (float)Reverb3dDecayHfRatioTrackBar.Value / 100.0F;
            parameters.DecayTime = (float)Reverb3dDecayTimeTrackBar.Value / 100.0F;
            parameters.Density = Reverb3dDensityTrackBar.Value;
            parameters.Diffusion = Reverb3dDiffusionTrackBar.Value;
            parameters.HfReference = Reverb3dHfReferenceTrackBar.Value;
            parameters.Reflections = Reverb3dReflectionsTrackBar.Value;
            parameters.ReflectionsDelay = (float)Reverb3dReflectionsDelayTrackBar.Value / 1000.0F;
            parameters.Reverb = Reverb3dReverbTrackBar.Value;
            parameters.ReverbDelay = (float)Reverb3dReverbDelayTrackBar.Value / 1000.0F;
            parameters.Room = Reverb3dRoomTrackBar.Value;
            parameters.RoomHf = Reverb3dRoomHfTrackBar.Value;
            parameters.RoomRolloffFactor = Reverb3dRoomRollOffFactorTrackBar.Value;
        }

        void LoadReverb3dEffect(EffectsInteractive3DLevel2Reverb parameters)
        {
            SetTrackBar(Reverb3dDecayHfRatioTrackBar, parameters.DecayHfRatio);
            SetTrackBar(Reverb3dDecayTimeTrackBar, parameters.DecayTime);
            SetTrackBar(Reverb3dDensityTrackBar, parameters.Density);
            SetTrackBar(Reverb3dDiffusionTrackBar, parameters.Diffusion);
            SetTrackBar(Reverb3dHfReferenceTrackBar, parameters.HfReference);
            SetTrackBar(Reverb3dReflectionsTrackBar, parameters.Reflections);
            SetTrackBar(Reverb3dReflectionsDelayTrackBar, parameters.ReflectionsDelay);
            SetTrackBar(Reverb3dReverbTrackBar, parameters.Reverb);
            SetTrackBar(Reverb3dReverbDelayTrackBar, parameters.ReverbDelay);
            SetTrackBar(Reverb3dRoomTrackBar, parameters.Room);
            SetTrackBar(Reverb3dRoomHfTrackBar, parameters.RoomHf);
            SetTrackBar(Reverb3dRoomRollOffFactorTrackBar, parameters.RoomRolloffFactor);
        }

        private void Reverb3dResetButton_Click(object sender, EventArgs e)
        {
            ResetReverb3d();
        }

        private void Reverb3dDecayHfRatioTrackBar_ValueChanged(object sender, EventArgs e)
        {
            Reverb3dDecayHfRatioValueLabel.Text = ((float)Reverb3dDecayHfRatioTrackBar.Value / 100.0F).ToString();
        }

        private void Reverb3dDecayTimeTrackBar_ValueChanged(object sender, EventArgs e)
        {
            Reverb3dDecayTimeValueLabel.Text = ((float)Reverb3dDecayTimeTrackBar.Value / 100.0F).ToString();
        }

        private void Reverb3dDensityTrackBar_ValueChanged(object sender, EventArgs e)
        {
            Reverb3dDensityValueLabel.Text = Reverb3dDensityTrackBar.Value.ToString();
        }

        private void Reverb3dDiffusionTrackBar_ValueChanged(object sender, EventArgs e)
        {
            Reverb3dDiffusionValueLabel.Text = Reverb3dDiffusionTrackBar.Value.ToString();
        }

        private void Reverb3dHfReferenceTrackBar_ValueChanged(object sender, EventArgs e)
        {
            Reverb3dHfReferenceValueLabel.Text = Reverb3dHfReferenceTrackBar.Value.ToString();
        }

        private void Reverb3dReflectionsTrackBar_ValueChanged(object sender, EventArgs e)
        {
            Reverb3dReflectionsValueLabel.Text = Reverb3dReflectionsTrackBar.Value.ToString();
        }

        private void Reverb3dReflectionsDelayTrackBar_ValueChanged(object sender, EventArgs e)
        {
            Reverb3dReflectionsDelayValueLabel.Text = ((float)Reverb3dReflectionsDelayTrackBar.Value / 1000.0F).ToString();
        }

        private void Reverb3dReverbTrackBar_ValueChanged(object sender, EventArgs e)
        {
            Reverb3dReverbValueLabel.Text = Reverb3dReverbTrackBar.Value.ToString();
        }

        private void Reverb3dReverbDelayTrackBar_ValueChanged(object sender, EventArgs e)
        {
            Reverb3dReverbDelayValueLabel.Text = ((float)Reverb3dReverbDelayTrackBar.Value / 1000.0F).ToString();
        }

        private void Reverb3dRoomTrackBar_ValueChanged(object sender, EventArgs e)
        {
            Reverb3dRoomValueLabel.Text = Reverb3dRoomTrackBar.Value.ToString();
        }

        private void Reverb3dRoomHfTrackBar_ValueChanged(object sender, EventArgs e)
        {
            Reverb3dRoomHfValueLabel.Text = Reverb3dRoomHfTrackBar.Value.ToString();
        }

        private void Reverb3dRoomRollOffFactorTrackBar_ValueChanged(object sender, EventArgs e)
        {
            Reverb3dRoomRollOffFactorValueLabel.Text = Reverb3dRoomRollOffFactorTrackBar.Value.ToString();
        }

        #endregion

        EffectsPreset _CurrentPreset;
        EffectsPreset CurrentPreset
        {
            get { return _CurrentPreset; }
            set
            {
                _CurrentPreset = value;
                var en = _CurrentPreset != null;
                GeneralSaveButton.Enabled = en;
                ChorusSaveButton.Enabled = en;
                CompressorSaveButton.Enabled = en;
                DistortionSaveButton.Enabled = en;
                EchoSaveButton.Enabled = en;
                FlangerSaveButton.Enabled = en;
                GargleSaveButton.Enabled = en;
                ParamEqSaveButton.Enabled = en;
                ReverbSaveButton.Enabled = en;
                Reverb3dSaveButton.Enabled = en;
            }
        }

        public void LoadPresetIntoForm(EffectsPreset preset)
        {
            CurrentPreset = preset;
            // Update General effect.
            GeneralCheckBox.Checked = preset.GeneralEnabled;
            LoadGeneralEffect(preset.General);
            // Update Chorus effect.
            ChorusCheckBox.Checked = preset.ChorusEnabled;
            LoadChorusEffect(preset.Chorus);
            // Update Compressor effect.
            CompressorCheckBox.Checked = preset.CompressorEnabled;
            LoadCompressorEffect(preset.Compressor);
            // Update Distortion effect.
            DistortionCheckBox.Checked = preset.DistortionEnabled;
            LoadDistortionEffect(preset.Distortion);
            // Update Echo effect.
            EchoCheckBox.Checked = preset.EchoEnabled;
            LoadEchoEffect(preset.Echo);
            // Update Flanger effect.
            FlangerCheckBox.Checked = preset.FlangerEnabled;
            LoadFlangerEffect(preset.Flanger);
            // Update Gargle effect.
            GargleCheckBox.Checked = preset.GargleEnabled;
            LoadGargleEffect(preset.Gargle);
            // Update ParamEq effect.
            ParamEqCheckBox.Checked = preset.ParamEqEnabled;
            LoadParamEqEffect(preset.ParamEq);
            // Update WavesReverb effect.
            ReverbCheckBox.Checked = preset.ReverbEnabled;
            LoadReverbEffect(preset.Reverb);
            // Update Interactive3DLevel2Reverb effect.
            Reverb3dCheckBox.Checked = preset.Reverb3DEnabled;
            LoadReverb3dEffect(preset.Reverb3D);
        }

        public void UpdatePresetFromForm(ref EffectsPreset preset)
        {
            // Update General effect.
            preset.GeneralEnabled = GeneralCheckBox.Checked;
            var General = preset.General;
            ApplyGeneralEffect(ref General);
            preset.General = General;
            // Update Chorus effect.
            preset.ChorusEnabled = ChorusCheckBox.Checked;
            var Chorus = preset.Chorus;
            ApplyChorusEffect(ref Chorus);
            preset.Chorus = Chorus;
            // Update Compressor effect.
            preset.CompressorEnabled = CompressorCheckBox.Checked;
            var Compressor = preset.Compressor;
            ApplyCompressorEffect(ref Compressor);
            preset.Compressor = Compressor;
            // Update Distortion effect.
            preset.DistortionEnabled = DistortionCheckBox.Checked;
            var Distortion = preset.Distortion;
            ApplyDistortionEffect(ref Distortion);
            preset.Distortion = Distortion;
            // Update Echo effect.
            preset.EchoEnabled = EchoCheckBox.Checked;
            var Echo = preset.Echo;
            ApplyEchoEffect(ref Echo);
            preset.Echo = Echo;
            // Update Flanger effect.
            preset.FlangerEnabled = FlangerCheckBox.Checked;
            var Flanger = preset.Flanger;
            ApplyFlangerEffect(ref Flanger);
            preset.Flanger = Flanger;
            // Update Gargle effect.
            preset.GargleEnabled = GargleCheckBox.Checked;
            var Gargle = preset.Gargle;
            ApplyGargleEffect(ref Gargle);
            preset.Gargle = Gargle;
            // Update ParamEq effect.
            preset.ParamEqEnabled = ParamEqCheckBox.Checked;
            var ParamEq = preset.ParamEq;
            ApplyParamEqEffect(ref ParamEq);
            preset.ParamEq = ParamEq;
            // Update WavesReverb effect.
            preset.ReverbEnabled = ReverbCheckBox.Checked;
            var Reverb = preset.Reverb;
            ApplyReverbEffect(ref Reverb);
            preset.Reverb = Reverb;
            // Update Interactive3DLevel2Reverb effect.
            preset.Reverb3DEnabled = Reverb3dCheckBox.Checked;
            var Reverb3D = preset.Reverb3D;
            ApplyReverb3dEffect(ref Reverb3D);
            preset.Reverb3D = Reverb3D;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            UpdatePresetFromForm(ref _CurrentPreset);
            EffectsPreset.SavePreset(_CurrentPreset);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                if (ApplicationDevice != null) ApplicationDevice.Dispose();
                if (ApplicationBuffer != null) ApplicationBuffer.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
