Imports Telerik.Web.UI

Partial Class Fondo_solidarieta_LoginFondoSolidarieta
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            par.caricaComboTelerik("SELECT * FROM ZONA_ALER where nominativo is not null order by zona", cmbZona, "COD", "ZONA")
            Dim script As String = "function f(){$find(""" + RadWindowInfoRU.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
        End If
    End Sub

    Protected Sub RadButtonLogin_Click(sender As Object, e As System.EventArgs) Handles RadButtonLogin.Click
        Try

            If cmbZona.SelectedValue = "-1" Or txtPassword.Text = "" Then
                lblErrore.Text = "Errore!"
                Dim script As String = "function f(){$find(""" + RadWindowInfoRU.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
            Else

                connData.apri()
                par.cmd.CommandText = "select * from siscom_mi.utenze_fondo_solidarieta where id_zona=" & cmbZona.SelectedValue & ""
                Dim lettore0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore0.Read Then
                    Dim PwMatch As Boolean = par.VerifyHash(txtPassword.Text, "SHA512", Trim(par.IfNull(lettore0("password"), ""))).ToString()
                    If PwMatch = True Then
                        Dim apertura As String = "location.replace('FondoSolidarieta.aspx?IDZ=" & cmbZona.SelectedValue & "', 'fSol' + '" & Format(Now, "yyyyMMddHHmmss") & "', '');"
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "apriFS", apertura, True)
                    Else
                        lblErrore.Text = "Errore!"
                        Dim script As String = "function f(){$find(""" + RadWindowInfoRU.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                        RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
                    End If
                End If
                lettore0.Close()
                connData.chiudi()
            End If


        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " RadButtonLogin_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
End Class
