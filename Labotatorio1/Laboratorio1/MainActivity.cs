using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;

namespace Laboratorio1
{
	[Activity(Label = "Laboratorio1", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		private Button btnIniciarApp;
		private EditText edtCorreoElectronico;
		private AlertDialog.Builder alert;
		private static string tagLaboratorio = "lab1";


		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			RequestWindowFeature(WindowFeatures.NoTitle);
			SetContentView(Resource.Layout.Main);

			btnIniciarApp = FindViewById<Button>(Resource.Id.btn_iniciar_anim);
			edtCorreoElectronico = FindViewById<EditText>(Resource.Id.edt_correo_electronico);

			btnIniciarApp.Click += delegate
			{
				if (validarInformacion())
				{
					ingresarRegistros(edtCorreoElectronico.Text);
				}
				else
				{
					Toast.MakeText(this, "Es necesario completar el formulario", ToastLength.Short).Show();
				}
			};
		}

		private bool validarInformacion()
		{
			return string.IsNullOrWhiteSpace(edtCorreoElectronico.Text) ? false : true;
		}

		private async void ingresarRegistros(string correoElectronico)
		{
			XamarinDiplomado.ServiceHelper helper = new XamarinDiplomado.ServiceHelper();
			await helper.InsertarEntidad(correoElectronico, tagLaboratorio, obtenerIdDispositivo());
			mostrarAlerta(correoElectronico);

		}


		private string obtenerIdDispositivo()
		{
			string idDevice = idDevice = Android.Provider.Settings.Secure.GetString(
				ContentResolver, Android.Provider.Settings.Secure.AndroidId);
			return idDevice;
		}


		private void mostrarAlerta(string correoElectronico)
		{
			alert = new AlertDialog.Builder(this);
			alert.SetTitle("Gracias por tu registro");
			alert.SetMessage("Correo electrónico registrado:  " + correoElectronico);
			alert.SetPositiveButton("Aceptar", (senderAlert, args) => { });
			Dialog dialog = alert.Create();
			dialog.Show();
		}

	}
}
