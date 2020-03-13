using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Views;
using ToolbarV7 = Android.Support.V7.Widget.Toolbar;
using VendaProduto.Classes;
using System.Collections.Generic;
using Newtonsoft.Json;
using VendaProduto.Adapters;
using Android.Content;

namespace VendaProduto.Activities
{
    /// <summary>
    /// Este activity exibe os pedidos feitos.
    /// </summary>
    [Activity()]
    public class PrincipalActivity : AppCompatActivity
    {
        ToolbarV7 tlbPrincipal;
        ListView lstPedidos;
        List<Pedido> pedidos;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            
            SetContentView(Resource.Layout.activity_principal);

            tlbPrincipal = FindViewById<ToolbarV7>(Resource.Id.tlbPrincipal);
            lstPedidos = FindViewById<ListView>(Resource.Id.lstPedidos);

            SetSupportActionBar(tlbPrincipal);
            SupportActionBar.Title = "Lista de Pedidos";

            pedidos = JsonConvert.DeserializeObject<List<Pedido>>(Intent.GetStringExtra("listaPedidos"));

            AdaptadorPedidos adp = new AdaptadorPedidos(this, pedidos);
            lstPedidos.Adapter = adp;

            lstPedidos.ItemClick += LstPedidos_ItemClick;

        }

        private void LstPedidos_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent tlProdPed = new Intent(this, typeof(ProdutoPedidoActivity));
            tlProdPed.PutExtra("produtosPedido", JsonConvert.SerializeObject(pedidos[e.Position]));
            StartActivityForResult(tlProdPed, 100);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.toolbar_principal, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.tlbItem_novoPedido:
                    Intent telaPed = new Intent(this, typeof(PedidoActivity));
                    StartActivityForResult(telaPed, 100);
                    break;
                case Resource.Id.tlbItem_gerProdutos:
                    Intent telaProd = new Intent(this, typeof(ProdutoActivity));
                    StartActivity(telaProd);
                    break;
                default:
                    Toast.MakeText(this, "Algum erro aconteceu...", ToastLength.Short).Show();
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }

        //TODO - O método OnActivityResult deverá ser criado para recarregar a lista de pedidos.


        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if (requestCode == 100 && resultCode == Result.Ok && data != null)
            {
                Pedido pedidoGerado = JsonConvert.DeserializeObject<Pedido>(data.GetStringExtra("pedidoGerado"));

                pedidos.Add(pedidoGerado);

                AdaptadorPedidos adp = new AdaptadorPedidos(this, pedidos);
                lstPedidos.Adapter = adp;
            }
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}