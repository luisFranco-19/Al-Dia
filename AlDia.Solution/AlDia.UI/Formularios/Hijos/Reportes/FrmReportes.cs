using AlDia.UI.Formularios.Base;

namespace AlDia.UI.Formularios.Hijos.Reportes;

// Diseño del centro de reportes; sin consultas, cálculos ni exportación.
public partial class FrmReportes : FrmListadoBase
{
    public FrmReportes()
    {
        InitializeComponent();
    }

    protected override bool MostrarBotonPrincipal => true;
    protected override Form CrearFormularioAccionPrincipal() => new FrmVistaReporte(tabReportes.SelectedIndex);
}
