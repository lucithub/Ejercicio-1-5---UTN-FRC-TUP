using Problema1_6.Domain;
using Problema1_6.Repositories;
using Problema1_6.Repositories.Implementations;
using Problema1_6.Services;
using System.Data;

public class Program
{
    public static void Main(string[] args)
    {
        BancoManager serviceManager = new BancoManager();
        var RepoTC = serviceManager.GetAllTiposCuentas();
        foreach (var item in RepoTC)
        {
            Console.WriteLine(item.ID + " " + item.Nombre);
        }

        //var RepoCli = serviceManager.GetAllClientes();
        //foreach (var item in RepoCli)
        //{
        //    Console.WriteLine($"ID: {item.ID}, Nombre: {item.Nombre}, Apellido: {item.Apellido}, DNI: {item.DNI}");
        //}

        //Cuentas oCuenta = new Cuentas();
        //oCuenta.ID = 1;
        //oCuenta.Saldo = 50000;
        //oCuenta.TipoCuenta = RepoTC[0];
        //oCuenta.CBU = "123456789";
        //oCuenta.Cliente = RepoCli[0];
        //oCuenta.UltimoMovimiento = DateTime.Now;
        //var repoCue = new CuentaRepository();
        //repoCue.Add(oCuenta);
        //foreach (var item in repoCue.GetAll())
        //{
        //    Console.WriteLine($"ID: {item.ID}, CBU: {item.CBU}, Saldo: {item.Saldo}, TipoCuenta: {item.TipoCuenta.ID}, UltimoMovimiento: {item.UltimoMovimiento}, Cliente: {item.Cliente.ID}");
        //}

        Clientes oCliente = new Clientes();
        oCliente.ID = 105695;
        oCliente.Nombre = "Lucas";
        oCliente.Apellido = "Velez";
        oCliente.DNI = "46849989";
        oCliente.ListaCuentas = new List<Cuentas>();
        Cuentas oCuenta2 = new Cuentas();
        oCuenta2.ID = 20555;
        oCuenta2.Saldo = 2;
        oCuenta2.TipoCuenta = RepoTC[0];
        oCuenta2.CBU = "153759";
        oCuenta2.Cliente = oCliente;
        oCuenta2.UltimoMovimiento = DateTime.Now;
        oCliente.ListaCuentas.Add(oCuenta2);
        serviceManager.AddCliente(oCliente);


    }
}