
Partial Class VSA_ControllaComponenti
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            Dim strScript As String = ""

            codFiscale.Value = Request.QueryString("CF")
            iddich.Value = Request.QueryString("IDDICH")
            If codFiscale.Value = "" Then
                lblMessaggio.Visible = True
                lblMessaggio.Text = "Inserire codice fiscale!"
            Else
                If CorrelazioniVSA(UCase(codFiscale.Value)) = True Then
                    strScript = "<script language='javascript'>self.close();window.open('Correlazioni3.aspx?" & "CF=" & par.VaroleDaPassare(codFiscale.Value) & "&ID=" & iddich.Value & "&V=1','Corr','top=320,left=420,width=600,height=400');" _
                    & "</script>"
                    Response.Write(strScript)

                    'lblMessaggio.Text = "Componente presente in GESTIONE LOCATARI!<br />Clicca <a href='javascript:void(0)' onclick=" & Chr(34) & "self.close();window.open('Correlazioni3.aspx?" & "CF=" & par.VaroleDaPassare(codFiscale.Value) & "&ID=" & iddich.Value & "&V=1','Corr','top=320,left=420,width=600,height=400');" & Chr(34) & ">qui</a> per visualizzare l'elenco delle correlazioni trovate."
                Else
                    lblMessaggio.Visible = True
                    lblMessaggio.Text = "Componente non presente in GESTIONE LOCATARI!"
                End If
            End If

        End If
    End Sub

    Private Function CorrelazioniVSA(ByVal CF As String) As Boolean
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            CorrelazioniVSA = False

            par.cmd.CommandText = "SELECT DICHIARAZIONI_vsa.ID FROM DICHIARAZIONI_vsa,COMP_NUCLEO_vsa WHERE DICHIARAZIONI_vsa.ID=COMP_NUCLEO_vsa.ID_DICHIARAZIONE (+) AND (COMP_NUCLEO_vsa.COD_FISCALE='" & codFiscale.Value & "' OR COMP_NUCLEO_vsa.COD_FISCALE='" & CF & "') AND COMP_NUCLEO_vsa.ID_DICHIARAZIONE<>" & iddich.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                CorrelazioniVSA = True
            End If
            myReader.Close()

            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try

    End Function
End Class
