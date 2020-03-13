using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using VendaProduto.Classes;

namespace VendaProduto.Activities
{
    [Activity()]
    public class PedidoActivity : AppCompatActivity
    {
        Spinner spnProdutos;
        Button btnAdicionarPedido;
        Button btnFinalizarPedido;

        List<Produto> produtos;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_pedido);

            spnProdutos = FindViewById<Spinner>(Resource.Id.spnProdutos);
            btnAdicionarPedido = FindViewById<Button>(Resource.Id.btnAdicionarPedido);
            btnFinalizarPedido = FindViewById<Button>(Resource.Id.btnFinalizarPedido);

            // Busquei todos os produtos do banco de dados
            // Estes foram armazenados numa Lista de produtos
            produtos = new Produto().BuscarTodosProdutos();

            // Declarei um ArrayList que vai receber somente o Nome dos produtos
            ArrayList aprod = new ArrayList();

            //Para cara produto (p) dentro da lista de produtos (produtos)
            //Adiciono o nome dentro do ArrayList aprod
            produtos.ForEach(delegate (Produto p) {
                aprod.Add(p.NomeProduto);
            });

            //Adapto o arraylist e armazeno no spinner
            ArrayAdapter adp = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, aprod);
            spnProdutos.Adapter = adp;

            #region Código exemplo ATENÇÃO!
            //Manipulando (extraíndo dados) do item selecionado do spinner
            //var asda = produtos[spnProdutos.SelectedItemPosition].PrecoUnit;
            //Toast.MakeText(this, asda.ToString(), ToastLength.Short).Show();
            #endregion



            //TODO - Adicionar um produto selecionado no spinner numa ListView ao clicar neste botão. 
            btnAdicionarPedido.Click += BtnAdicionarPedido_Click;

            //TODO - Este botão irá gerar um novo pedido e vincular os produtos ao pedido. 
            btnFinalizarPedido.Click += BtnFinalizarPedido_Click;
        }

        private void BtnFinalizarPedido_Click(object sender, EventArgs e)
        {

        }

        private void BtnAdicionarPedido_Click(object sender, EventArgs e)
        {

        }
    }
}