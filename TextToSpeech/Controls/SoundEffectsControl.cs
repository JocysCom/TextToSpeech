using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using JocysCom.TextToSpeech.Monitor.Audio;
using SharpDX.DirectSound;

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

        private SecondarySoundBuffer ApplicationBuffer = null;
        private DirectSound ApplicationDevice = null;

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
            // Create and set the sound device.
            ApplicationDevice = new DirectSound();
            ApplicationDevice.SetCooperativeLevel(this.Handle, CooperativeLevel.Normal);
        }

        public void LoadSoundFile(byte[] bytes, int sampleRate, int bitsPerSample, int channelCount)
        {
			// Create and set the buffer description.
			var buffer_desc = new SoundBufferDescription();
			var format = new SharpDX.Multimedia.WaveFormat(sampleRate, bitsPerSample, channelCount);
			buffer_desc.Format = format;
			buffer_desc.Flags =
				// Play sound even if application loses focus.
				BufferFlags.GlobalFocus |
                // This has to be true to use effects.
                BufferFlags.ControlEffects;
			buffer_desc.BufferBytes = bytes.Length;

			// Create and set the buffer for playing the sound.
			ApplicationBuffer = new SecondarySoundBuffer(ApplicationDevice, buffer_desc);
            ApplicationBuffer.Write(bytes, 0, LockFlags.None);
            //var ms = new MemoryStream(bytes);
            //ms.Position = 0;
            //LoadSoundFile(ms, sampleRate, bitsPerSample, channelCount);
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

		Guid StandardFlangerGuid = new Guid("efca3d92-dfd8-4672-a603-7420894bad98");
		Guid StandardChorusGuid = new Guid("efe6629c-81f7-4281-bd91-c9d604a95af6");
		Guid StandardCompressorGuid = new Guid("ef011f79-4000-406d-87af-bffb3fc39d57");
		Guid StandardI3DL2ReverbGuid = new Guid("ef985e71-d5c7-42d4-ba4d-2d073e2e96f4");
		Guid StandardWavesReverbGuid = new Guid("87fc0268-9a55-4360-95aa-004a1d9de26c");
		Guid StandardGargleGuid = new Guid("dafd8210-5711-4b91-9fe3-f75b7ae279bf");
		Guid StandardEchoGuid = new Guid("ef3e932c-d40b-4f51-8ccf-3f98f1b29d5d");
		Guid StandardParameqGuid = new Guid("120ced89-3bf4-4173-a132-3cb406cf3231");
		Guid StandardDistortionGuid = new Guid("ef114c90-cd1d-484e-96e5-09cfaf912a21");

		public void PlaySound()
        {

            if (ApplicationBuffer != null)
            {
                if (EffectsCount > 0)
                {
                    int arrayCount = 0;
                    Guid[] effects = new Guid[EffectsCount];
					if (ChorusCheckBox.Checked)
					{
						effects[arrayCount] = StandardChorusGuid;
						arrayCount += 1;
					}
					if (CompressorCheckBox.Checked)
					{
						effects[arrayCount] = StandardCompressorGuid;
						arrayCount += 1;
					}
					if (DistortionCheckBox.Checked)
					{
						effects[arrayCount] = StandardDistortionGuid;
						arrayCount += 1;
					}
					if (EchoCheckBox.Checked)
					{
						effects[arrayCount] = StandardEchoGuid;
						arrayCount += 1;
					}
					if (FlangerCheckBox.Checked)
					{
						effects[arrayCount] = StandardFlangerGuid;
						arrayCount += 1;
					}
					if (GargleCheckBox.Checked)
					{
						effects[arrayCount] = StandardGargleGuid;
						arrayCount += 1;
					}
					if (ParamEqCheckBox.Checked)
					{
						effects[arrayCount] = StandardParameqGuid;
						arrayCount += 1;
					}
					if (ReverbCheckBox.Checked)
					{
						effects[arrayCount] = StandardWavesReverbGuid;
						arrayCount += 1;
					}
					if (Reverb3dCheckBox.Checked)
					{
						effects[arrayCount] = StandardI3DL2ReverbGuid;
						arrayCount += 1;
					}
					ApplicationBuffer.SetEffect(effects);
                    arrayCount = 0;
                    if (ChorusCheckBox.Checked)
                    {
                        var chorus = ApplicationBuffer.GetEffect<Chorus>(arrayCount);
                        var chorusParamaters = chorus.AllParameters;
                        ApplyChorusEffect(ref chorusParamaters);
                        chorus.AllParameters = chorusParamaters;
                        arrayCount += 1;
                    }
                    if (CompressorCheckBox.Checked)
                    {
                        var compressor = ApplicationBuffer.GetEffect<Compressor>(arrayCount);
                        var compressorParamaters = compressor.AllParameters;
                        ApplyCompressorEffect(ref compressorParamaters);
                        compressor.AllParameters = compressorParamaters;
                        arrayCount += 1;
                    }
                    if (DistortionCheckBox.Checked)
                    {
                        var distortion = ApplicationBuffer.GetEffect<Distortion>(arrayCount);
                        var distortionParamaters = distortion.AllParameters;
                        ApplyDistortionEffect(ref distortionParamaters);
                        distortion.AllParameters = distortionParamaters;
                        arrayCount += 1;
                    }
                    if (EchoCheckBox.Checked)
                    {
                        var echo = ApplicationBuffer.GetEffect<Echo>(arrayCount);
                        var echoParamaters = echo.AllParameters;
                        ApplyEchoEffect(ref echoParamaters);
                        echo.AllParameters = echoParamaters;
                        arrayCount += 1;
                    }
                    if (FlangerCheckBox.Checked)
                    {
                        var flanger = ApplicationBuffer.GetEffect<Flanger>(arrayCount);
                        var flangerParamaters = flanger.AllParameters;
                        ApplyFlangerEffect(ref flangerParamaters);
                        flanger.AllParameters = flangerParamaters;
                        arrayCount += 1;
                    }
                    if (GargleCheckBox.Checked)
                    {
                        var gargle = ApplicationBuffer.GetEffect<Gargle>(arrayCount);
                        var gargleParamaters = gargle.AllParameters;
                        ApplyGargleEffect(ref gargleParamaters);
                        gargle.AllParameters = gargleParamaters;
                        arrayCount += 1;
                    }
                    if (ParamEqCheckBox.Checked)
                    {
                        var paramEq = ApplicationBuffer.GetEffect<ParametricEqualizer>(arrayCount);
                        var paramEqParamaters = paramEq.AllParameters;
                        ApplyParamEqEffect(ref paramEqParamaters);
                        paramEq.AllParameters = paramEqParamaters;
                        arrayCount += 1;
                    }
                    if (ReverbCheckBox.Checked)
                    {
                        var reverb = ApplicationBuffer.GetEffect<WavesReverb>(arrayCount);
                        var reverbParamaters = reverb.AllParameters;
                        ApplyReverbEffect(ref reverbParamaters);
                        reverb.AllParameters = reverbParamaters;
                        arrayCount += 1;
                    }
                    if (Reverb3dCheckBox.Checked)
                    {
                        var reverb3D = ApplicationBuffer.GetEffect<I3DL2Reverb>(arrayCount);
                        var reverb3DParamaters = reverb3D.AllParameters;
                        ApplyReverb3dEffect(ref reverb3DParamaters);
                        reverb3D.AllParameters = reverb3DParamaters;
                        arrayCount += 1;
                    }

                }
            }
            if (null != ApplicationBuffer)
            {
                ApplicationBuffer.Play(0, PlayFlags.None);
            }
        }

        private void mnuStop_Click(object sender, System.EventArgs e)
        {

            int currentPlayPosition;
            int currentWritePosition;
            ApplicationBuffer.GetCurrentPosition(out currentPlayPosition, out currentWritePosition);
            if (null != ApplicationBuffer && currentPlayPosition != 0)
            {
                ApplicationBuffer.Volume = -10000;
                ApplicationBuffer.Stop();
                ApplicationBuffer.CurrentPosition = 0;
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
            ChorusPhases.Add(ChorusPhaseNegative180RadioButton, Chorus.PhaseNegative180);
            ChorusPhases.Add(ChorusPhaseNegative90RadioButton, Chorus.PhaseNegative90);
            ChorusPhases.Add(ChorusPhaseZeroRadioButton, Chorus.PhaseZero);
            ChorusPhases.Add(ChorusPhase90RadioButton, Chorus.Phase90);
            ChorusPhases.Add(ChorusPhase180RadioButton, Chorus.Phase180);
            ChorusWaveforms.Clear();
            ChorusWaveforms.Add(ChorusWaveSinRadioButton, Chorus.WaveformSin);
            ChorusWaveforms.Add(ChorusWaveTriangleRadioButton, Chorus.WaveformTriangle);
            ResetTrackBar(ChorusDelayTrackBar, Chorus.DelayMin, Chorus.DelayMax, 16F, 10m);
            ResetTrackBar(ChorusDepthTrackBar, Chorus.DepthMin, Chorus.DepthMax, 10F);
            ResetTrackBar(ChorusFeedbackTrackBar, Chorus.FeedbackMin, Chorus.FeedbackMax, 25F);
            ResetTrackBar(ChorusFrequencyTrackBar, Chorus.FrequencyMin, Chorus.FrequencyMax, 1.1F, 10m);
            ResetTrackBar(ChorusWetDryMixTrackBar, Chorus.WetDryMixMin, Chorus.WetDryMixMax, 50F);
            ChorusPhase90RadioButton.Checked = true;
            ChorusWaveSinRadioButton.Checked = true;
        }

        void ApplyChorusEffect(ref ChorusSettings parameters)
        {
            parameters.Delay = (float)ChorusDelayTrackBar.Value / 10.0F;
            parameters.Depth = ChorusDepthTrackBar.Value;
            parameters.Feedback = ChorusFeedbackTrackBar.Value;
            parameters.Frequency = ChorusFrequencyTrackBar.Value / 10.0F;
            parameters.WetDryMix = ChorusWetDryMixTrackBar.Value;
            parameters.Phase = ChorusPhases.FirstOrDefault(x => x.Key.Checked).Value;
            parameters.Waveform = ChorusWaveforms.FirstOrDefault(x => x.Key.Checked).Value;
        }

        void LoadChorusEffect(ChorusSettings parameters)
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
            ResetTrackBar(CompressorAttackTrackBar, Compressor.AttackMin, Compressor.AttackMax, 10F);
            ResetTrackBar(CompressorGainTrackBar, Compressor.GainMin, Compressor.GainMax, 0F);
            ResetTrackBar(CompressorPreDelayTrackBar, Compressor.PreDelayMin, Compressor.PreDelayMax, 4F);
            ResetTrackBar(CompressorRatioTrackBar, Compressor.RatioMin, Compressor.RatioMax, 3F);
            ResetTrackBar(CompressorReleaseTrackBar, Compressor.ReleaseMin, Compressor.ReleaseMax, 200F);
            ResetTrackBar(CompressorThresholdTrackBar, Compressor.ThresholdMin, Compressor.ThresholdMax, -20F);
        }

        void ApplyCompressorEffect(ref CompressorSettings parameters)
        {
            parameters.Attack = CompressorAttackTrackBar.Value;
            parameters.Gain = CompressorGainTrackBar.Value;
            parameters.Predelay = CompressorPreDelayTrackBar.Value;
            parameters.Ratio = CompressorRatioTrackBar.Value;
            parameters.Release = CompressorReleaseTrackBar.Value;
            parameters.Threshold = CompressorThresholdTrackBar.Value;
        }

        void LoadCompressorEffect(CompressorSettings parameters)
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
            ResetTrackBar(DistortionEdgeTrackBar, Distortion.EdgeMin, Distortion.EdgeMax, 15F);
            ResetTrackBar(DistortionGainTrackBar, Distortion.GainMin, Distortion.GainMax, -18F);
            ResetTrackBar(DistortionBandwidthTrackBar, Distortion.PostEQBandwidthMin, Distortion.PostEQBandwidthMax, 2400F);
            ResetTrackBar(DistortionFrequencyTrackBar, Distortion.PostEQCenterFrequencyMin, Distortion.PostEQCenterFrequencyMax, 2400F);
            ResetTrackBar(DistortionLowPassTrackBar, Distortion.PreLowPassCutoffMin, Distortion.PreLowPassCutoffMax, 8000F);
        }

        void ApplyDistortionEffect(ref DistortionSettings parameters)
        {
            parameters.Edge = DistortionEdgeTrackBar.Value;
            parameters.Gain = DistortionGainTrackBar.Value;
            parameters.PostEQBandwidth = DistortionBandwidthTrackBar.Value;
            parameters.PostEQCenterFrequency = DistortionFrequencyTrackBar.Value;
            parameters.PreLowpassCutoff = DistortionLowPassTrackBar.Value;
        }

        void LoadDistortionEffect(DistortionSettings parameters)
        {
            SetTrackBar(DistortionEdgeTrackBar, parameters.Edge);
            SetTrackBar(DistortionGainTrackBar, parameters.Gain);
            SetTrackBar(DistortionBandwidthTrackBar, parameters.PostEQBandwidth);
            SetTrackBar(DistortionFrequencyTrackBar, parameters.PostEQCenterFrequency);
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
            ResetTrackBar(EchoFeedbackTrackBar, Echo.FeedbackMin, Echo.FeedbackMax, 50F);
            ResetTrackBar(EchoLeftDelayTrackBar, Echo.LeftDelayMin, Echo.LeftDelayMax, 100F);
            ResetTrackBar(EchoRightDelayTrackBar, Echo.RightDelayMin, Echo.RightDelayMax, 300F);
            ResetTrackBar(EchoWetDryMixTrackBar, Echo.WetDryMixMin, Echo.WetDryMixMax, 50F);
            EchoPanDelayCheckBox.Checked = false;
        }

        void ApplyEchoEffect(ref EchoSettings parameters)
        {
            parameters.Feedback = EchoFeedbackTrackBar.Value;
            parameters.LeftDelay = EchoLeftDelayTrackBar.Value;
            parameters.PanDelay = EchoPanDelayCheckBox.Checked ? Echo.PanDelayMax : Echo.PanDelayMin;
            parameters.LeftDelay = EchoRightDelayTrackBar.Value;
            parameters.WetDryMix = EchoWetDryMixTrackBar.Value;
        }

        void LoadEchoEffect(EchoSettings parameters)
        {
            SetTrackBar(EchoFeedbackTrackBar, parameters.Feedback);
            SetTrackBar(EchoLeftDelayTrackBar, parameters.LeftDelay);
            SetTrackBar(EchoRightDelayTrackBar, parameters.RightDelay);
            SetTrackBar(EchoWetDryMixTrackBar, parameters.WetDryMix);
            EchoPanDelayCheckBox.Checked = (parameters.PanDelay == Echo.PanDelayMax);
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
            FlangerPhases.Add(FlangerPhaseNegative180RadioButton, Flanger.PhaseNegative180);
            FlangerPhases.Add(FlangerPhaseNegative90RadioButton, Flanger.PhaseNegative90);
            FlangerPhases.Add(FlangerPhaseZeroRadioButton, Flanger.PhaseZero);
            FlangerPhases.Add(FlangerPhase90RadioButton, Flanger.Phase90);
            FlangerPhases.Add(FlangerPhase180RadioButton, Flanger.Phase180);
            FlangerWaveforms.Clear();
            FlangerWaveforms.Add(FlangerWaveSinRadioButton, Flanger.WaveformSin);
            FlangerWaveforms.Add(FlangerWaveTriangleRadioButton, Flanger.WaveformTriangle);
            ResetTrackBar(FlangerDelayTrackBar, Flanger.DelayMin, Flanger.DelayMax, 2F, 10m);
            ResetTrackBar(FlangerDepthTrackBar, Flanger.DepthMin, Flanger.DepthMax, 100F);
            ResetTrackBar(FlangerFeedbackTrackBar, Flanger.FeedbackMin, Flanger.FeedbackMax, -50F);
            ResetTrackBar(FlangerFrequencyTrackBar, Flanger.FrequencyMin, Flanger.FrequencyMax, 0F, 10m);
            ResetTrackBar(FlangerWetDryMixTrackBar, Flanger.WetDryMixMin, Flanger.WetDryMixMax, 50F);
            FlangerPhase90RadioButton.Checked = true;
            FlangerWaveSinRadioButton.Checked = true;
        }

        void ApplyFlangerEffect(ref FlangerSettings parameters)
        {
            parameters.Delay = (float)FlangerDelayTrackBar.Value / 10.0F;
            parameters.Depth = FlangerDepthTrackBar.Value;
            parameters.Feedback = FlangerFeedbackTrackBar.Value;
            parameters.Frequency = (float)FlangerFrequencyTrackBar.Value / 10.0F;
            parameters.Phase = FlangerPhases.FirstOrDefault(x => x.Key.Checked).Value;
            parameters.WetDryMix = FlangerWetDryMixTrackBar.Value;
            parameters.Waveform = FlangerWaveforms.FirstOrDefault(x => x.Key.Checked).Value;
        }

        void LoadFlangerEffect(FlangerSettings parameters)
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
            GargleWaveShapes.Add(GargleWaveSquareRadioButton, Gargle.WaveShapeSquare);
            GargleWaveShapes.Add(GargleWaveTriangleRadioButton, Gargle.WaveShapeTriangle);
            ResetTrackBar(GargleRateHzTrackBar, Gargle.RateMin, Gargle.RateMax, 20F);
            GargleWaveTriangleRadioButton.Checked = true;
        }

        void ApplyGargleEffect(ref GargleSettings parameters)
        {
            parameters.RateHz = GargleRateHzTrackBar.Value;
            parameters.WaveShape = GargleWaveShapes.FirstOrDefault(x => x.Key.Checked).Value;
        }

        void LoadGargleEffect(GargleSettings parameters)
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
            ResetTrackBar(ParamEqBandwidthTrackBar, ParametricEqualizer.BandwidthMin, ParametricEqualizer.BandwidthMax, 12F);
            ResetTrackBar(ParamEqCenterTrackBar, ParametricEqualizer.CenterMin, ParametricEqualizer.CenterMax, 8000F);
            ResetTrackBar(ParamEqGainTrackBar, ParametricEqualizer.GainMin, ParametricEqualizer.GainMax, 0F);
        }

        void ApplyParamEqEffect(ref ParametricEqualizerSettings parameters)
        {
            parameters.Bandwidth = ParamEqBandwidthTrackBar.Value;
            parameters.Center = ParamEqCenterTrackBar.Value;
            parameters.Gain = ParamEqGainTrackBar.Value;
        }

        void LoadParamEqEffect(ParametricEqualizerSettings parameters)
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
            ResetTrackBar(ReverbRatioTrackBar, WavesReverb.HighFrequencyRTRatioMin, WavesReverb.HighFrequencyRTRatioMax, WavesReverb.HighFrequencyRTRatioDefault, 1000m);
            ResetTrackBar(ReverbInputGainTrackBar, WavesReverb.InGainMin, WavesReverb.InGainMax, WavesReverb.InGainDefault);
            ResetTrackBar(ReverbMixTrackBar, WavesReverb.ReverbMixMin, WavesReverb.ReverbMixMax, WavesReverb.ReverbMixDefault);
            ResetTrackBar(ReverbTimeTrackBar, WavesReverb.ReverbTimeMin, WavesReverb.ReverbTimeMax, WavesReverb.ReverbTimeDefault);
        }

        void ApplyReverbEffect(ref WavesReverbSettings parameters)
        {
            parameters.HighFreqRTRatio = (float)ReverbRatioTrackBar.Value / 1000.0F;
            parameters.InGain = ReverbInputGainTrackBar.Value;
            parameters.ReverbMix = ReverbMixTrackBar.Value;
            parameters.ReverbTime = ReverbTimeTrackBar.Value;
        }

        void LoadReverbEffect(WavesReverbSettings parameters)
        {
            SetTrackBar(ReverbRatioTrackBar, parameters.HighFreqRTRatio);
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
            ResetTrackBar(Reverb3dDecayHfRatioTrackBar, I3DL2Reverb.DecayHFRatioMin, I3DL2Reverb.DecayHFRatioMax, I3DL2Reverb.DecayHFRatioDefault, 100m);
            ResetTrackBar(Reverb3dDecayTimeTrackBar, I3DL2Reverb.DecayTimeMin, I3DL2Reverb.DecayTimeMax, I3DL2Reverb.DecayTimeDefault, 100m);
            ResetTrackBar(Reverb3dDensityTrackBar, I3DL2Reverb.DensityMin, I3DL2Reverb.DensityMax, I3DL2Reverb.DensityDefault);
            ResetTrackBar(Reverb3dDiffusionTrackBar, I3DL2Reverb.DiffusionMin, I3DL2Reverb.DiffusionMax, I3DL2Reverb.DiffusionDefault);
            ResetTrackBar(Reverb3dHfReferenceTrackBar, I3DL2Reverb.HFReferenceMin, I3DL2Reverb.HFReferenceMax, I3DL2Reverb.HFReferenceDefault);
            ResetTrackBar(Reverb3dReflectionsTrackBar, I3DL2Reverb.ReflectionsMin, I3DL2Reverb.ReflectionsMax, I3DL2Reverb.ReflectionsDefault);
            ResetTrackBar(Reverb3dReflectionsDelayTrackBar, I3DL2Reverb.ReflectionsDelayMin, I3DL2Reverb.ReflectionsDelayMax, I3DL2Reverb.ReflectionsDelayDefault, 1000m);
            ResetTrackBar(Reverb3dReverbTrackBar, I3DL2Reverb.ReverbMin, I3DL2Reverb.ReverbMax, I3DL2Reverb.ReverbDefault);
            ResetTrackBar(Reverb3dReverbDelayTrackBar, I3DL2Reverb.ReverbDelayMin, I3DL2Reverb.ReverbDelayMax, I3DL2Reverb.ReverbDelayDefault, 1000m);
            ResetTrackBar(Reverb3dRoomTrackBar, I3DL2Reverb.RoomMin, I3DL2Reverb.RoomMax, I3DL2Reverb.RoomDefault);
            ResetTrackBar(Reverb3dRoomHfTrackBar, I3DL2Reverb.RoomHFMin, I3DL2Reverb.RoomHFMax, I3DL2Reverb.RoomHFDefault);
            ResetTrackBar(Reverb3dRoomRollOffFactorTrackBar, I3DL2Reverb.RoomRolloffFactorMin, I3DL2Reverb.RoomRolloffFactorMax, I3DL2Reverb.RoomRolloffFactorDefault);
        }

        void ApplyReverb3dEffect(ref I3DL2ReverbSettings parameters)
        {
            parameters.DecayHFRatio = (float)Reverb3dDecayHfRatioTrackBar.Value / 100.0F;
            parameters.DecayTime = (float)Reverb3dDecayTimeTrackBar.Value / 100.0F;
            parameters.Density = Reverb3dDensityTrackBar.Value;
            parameters.Diffusion = Reverb3dDiffusionTrackBar.Value;
            parameters.HFReference = Reverb3dHfReferenceTrackBar.Value;
            parameters.Reflections = Reverb3dReflectionsTrackBar.Value;
            parameters.ReflectionsDelay = (float)Reverb3dReflectionsDelayTrackBar.Value / 1000.0F;
            parameters.Reverb = Reverb3dReverbTrackBar.Value;
            parameters.ReverbDelay = (float)Reverb3dReverbDelayTrackBar.Value / 1000.0F;
            parameters.Room = Reverb3dRoomTrackBar.Value;
            parameters.RoomHF = Reverb3dRoomHfTrackBar.Value;
            parameters.RoomRolloffFactor = Reverb3dRoomRollOffFactorTrackBar.Value;
        }

        void LoadReverb3dEffect(I3DL2ReverbSettings parameters)
        {
            SetTrackBar(Reverb3dDecayHfRatioTrackBar, parameters.DecayHFRatio);
            SetTrackBar(Reverb3dDecayTimeTrackBar, parameters.DecayTime);
            SetTrackBar(Reverb3dDensityTrackBar, parameters.Density);
            SetTrackBar(Reverb3dDiffusionTrackBar, parameters.Diffusion);
            SetTrackBar(Reverb3dHfReferenceTrackBar, parameters.HFReference);
            SetTrackBar(Reverb3dReflectionsTrackBar, parameters.Reflections);
            SetTrackBar(Reverb3dReflectionsDelayTrackBar, parameters.ReflectionsDelay);
            SetTrackBar(Reverb3dReverbTrackBar, parameters.Reverb);
            SetTrackBar(Reverb3dReverbDelayTrackBar, parameters.ReverbDelay);
            SetTrackBar(Reverb3dRoomTrackBar, parameters.Room);
            SetTrackBar(Reverb3dRoomHfTrackBar, parameters.RoomHF);
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
