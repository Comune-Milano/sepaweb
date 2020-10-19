
Partial Class Contratti_Canone
    Inherits UserControlSetIdMode



    Public Sub disattivaTutto()
        txtCanoneIniziale.ReadOnly = True
        txtCanoneCorrente.ReadOnly = True
        txtImportoCauzione.ReadOnly = True
        'txtLibrettoDeposito.ReadOnly = True
        cmbFreqCanone.Enabled = False
        cmbDestRate.Enabled = False
        checkConguaglioBoll.Enabled = False
        'checkInteressiCauzione.Enabled = False
        checkInvioBoll.Enabled = False
        chRitardoPagamenti.Enabled = False
        txtImportoAnticipo.ReadOnly = True
        lblistat2.Enabled = False
        lblistat4.Enabled = False
        txtLibrettoDeposito.ReadOnly = True
        ImgRicalcolaC.Visible = False

    End Sub

    Public Function disattiva()
        txtCanoneIniziale.ReadOnly = True
        txtCanoneCorrente.ReadOnly = True
        txtImportoCauzione.ReadOnly = True
        txtImportoAnticipo.ReadOnly = True
        ImgRicalcolaC.Visible = False
    End Function

    Public Function Attiva()
        'txtCanoneIniziale.ReadOnly = False
        'txtCanoneCorrente.ReadOnly = False
        txtImportoCauzione.ReadOnly = False
        txtImportoAnticipo.ReadOnly = False
        'ImgRicalcolaC.Visible = True
    End Function


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
    End Sub

    Protected Sub ImgRicalcolaC_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgRicalcolaC.Click
        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        If HH1.Value = "1" Then
            CType(Me.Page, Object).RicalcolaCanone()
        End If
    End Sub
End Class
