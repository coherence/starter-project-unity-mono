


#region Interpolation
// -----------------------------------
//  Interpolation.cs
// -----------------------------------
			
namespace Coherence.Generated
{
	using global::Unity.Collections;

	public enum InterpolationSetupID
	{
		DefaultTranslation = 0,
		DefaultRotation = 1,
	}

	public static class Interpolation
	{
		//interpolation setups
		private static bool _doneSetup;
		private static NativeArray<InterpolationSetup> _interpolationSetups;

		public static void CreateSetups()
		{
			if (!_doneSetup)
			{
				_interpolationSetups = new NativeArray<InterpolationSetup>(2, Allocator.Persistent);

				_interpolationSetups[(int)InterpolationSetupID.DefaultTranslation] = new InterpolationSetup
				{
					overshootPercentage = 20f,
					overshootRetraction = 0.25f,
					manualLatency = 1f,
					autoLatencyFactor = 1.1f,
					elasticity = 0.2f,
					maxDistance = 1024f,
					curveType = InterpolationCurveType.CatmullRom,
				};

				_interpolationSetups[(int)InterpolationSetupID.DefaultRotation] = new InterpolationSetup
				{
					overshootPercentage = 20f,
					overshootRetraction = 0.25f,
					manualLatency = 1f,
					autoLatencyFactor = 1.1f,
					elasticity = 1.1f,
					maxDistance = 90f,
					curveType = InterpolationCurveType.Linear,
				};

				_doneSetup = true;
			}
		}

		public static void DestroySetups()
		{
			if (_doneSetup)
			{
				_interpolationSetups.Dispose();
				_doneSetup = false;
			}
		}

		public static NativeArray<InterpolationSetup>.ReadOnly GetSetups()
		{
			return _interpolationSetups.AsReadOnly();
		}

		public static InterpolationSetup GetSetup(InterpolationSetupID ID)
		{
			return _interpolationSetups[(int)ID];
		}
	}
}

// ------------------ end of Interpolation.cs -----------------
#endregion
