
Partial Class com_detrazioni
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalva.Click
        L3.Visible = False

        If cmbTipoReddito.SelectedItem.Value = "-1" Then
            L3.Visible = True
            L3.Text = "(Specificare Tipo)"
            Exit Sub
        End If

        If par.IfEmpty(txtImporto.Text, "") = "" Then
            L3.Visible = True
            L3.Text = "(Valore Numerico)"
            Exit Sub
        End If

        If IsNumeric(txtImporto.Text) = False Then
            L3.Visible = True
            L3.Text = "(Valore Numerico)"
            Exit Sub
        End If

        If InStr(txtImporto.Text, ".") > 0 Then
                L3.Visible = True
            L3.Text = "(Valore interi)"
            Exit Sub
        End If

        If InStr(txtImporto.Text, ",") > 0 Then
            L3.Visible = True
            L3.Text = "(Valore interi)"
            Exit Sub
        End If


        If L3.Visible = True Then
            Exit Sub
        End If

        Cache(Session.Item("GRiga")) = txtRiga.Text
        Cache(Session.Item("GProgressivo")) = cmbComponente.SelectedItem.Value
        Cache(Session.Item("GLista")) = par.MiaFormat(cmbComponente.SelectedItem.Text, 30) & " " & par.MiaFormat(cmbTipoReddito.SelectedItem.Text, 15) & " " & par.MiaFormat(cmbDetrazione.SelectedItem.Text, 35) & " " & par.MiaFormat(txtImporto.Text, 8) & ",00 "

        Response.Clear()
        Response.Write("<script>window.close();</script>")
        Response.End()


    End Sub

    Private Sub CaricaTipologie()
        par.caricaComboBox("select * from t_tipo_detrazioni order by COD asc", cmbDetrazione, "COD", "DESCRIZIONE")
    End Sub

    Private Function RicavaIndice(ByVal Testo As String) As Integer
        Try
            RicavaIndice = -1
            par.OracleConn.Open()
            par.SettaCommand(par)
            par.cmd.CommandText = "select cod from t_tipo_detrazioni where upper(descrizione) like '" & UCase(Testo) & "%'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                RicavaIndice = myReader("cod")
            End If
            myReader.Close()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
           
        End Try
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim COMPONENTE As String

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        Response.Write("<script></script>")
        If Not IsPostBack = True Then

            txtOperazione.Text = Request.QueryString("OP")
            COMPONENTE = par.Elimina160(Request.QueryString("COMPONENTE"))
            Riempi(COMPONENTE)
            CaricaTipologie()
            txtRiga.Text = par.Elimina160(Request.QueryString("RI"))



            If txtOperazione.Text = "1" Then
                txtImporto.Text = par.Elimina160(Request.QueryString("IM"))

                cmbDetrazione.ClearSelection()
                'cmbDetrazione.Items.FindByText(par.Elimina160(Request.QueryString("TI"))).Selected = True
                cmbDetrazione.Items.FindByValue(RicavaIndice(par.Elimina160(Request.QueryString("TI")))).Selected = True
                cmbTipoReddito.ClearSelection()
                cmbTipoReddito.Items.FindByText(par.Elimina160(Request.QueryString("TIPODET"))).Selected = True

                'If par.Elimina160(Request.QueryString("TI")) = "Spese per ricorvero in strutture so" Then
                '    cmbDetrazione.Items.FindByValue("2").Selected = True
                'Else
                '    If par.Elimina160(Request.QueryString("TI")) = "RPEF" Then
                '        cmbDetrazione.Items.FindByText("IRPEF").Selected = True
                '    Else
                '        cmbDetrazione.Items.FindByText(par.Elimina160(Request.QueryString("TI"))).Selected = True
                '    End If
                'End If

                cmbComponente.Items.FindByValue(par.Elimina160(Request.QueryString("COMP"))).Selected = True
                cmbComponente.Enabled = False
            End If

        End If
    End Sub

    Function Riempi(ByVal testo As String)
        Dim pos As Integer
        Dim Valore1 As String
        Dim Valore2 As String

        pos = 1

        Valore1 = ""
        Valore2 = ""
        Do While pos <= Len(testo)
            If Mid(testo, pos, 1) <> ";" Then
                Valore1 = Valore1 & Mid(testo, pos, 1)
            Else
                pos = pos + 1
                Do While pos <= Len(testo)
                    If Mid(testo, pos, 1) <> "!" Then
                        Valore2 = Valore2 & Mid(testo, pos, 1)
                    Else
                        cmbComponente.Items.Add(New ListItem(Valore1, Valore2))
                        Valore1 = ""
                        Valore2 = ""
                        Exit Do
                    End If
                    pos = pos + 1
                Loop
            End If
            pos = pos + 1
        Loop
    End Function

    Protected Sub cmbTipoReddito_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoReddito.SelectedIndexChanged

    End Sub
End Class
