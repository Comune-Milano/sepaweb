
Partial Class ANAUT_com_detrazioni
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalva.Click
        If IsNumeric(txtImporto.Text) = True Then
            L3.Visible = False
        Else
            L3.Visible = True
            L3.Text = "(Valore Numerico)"
            Exit Sub
        End If

        If InStr(txtImporto.Text, ".") = 0 Then
            L3.Visible = False
            If InStr(txtImporto.Text, ",") = 0 Then
                L3.Visible = False
            Else
                L3.Visible = True
                L3.Text = "(Valore interi)"
            End If
        Else
            L3.Visible = True
            L3.Text = "(Valore interi)"
        End If

        If L3.Visible = True Then
            Exit Sub
        End If

        If (cmbComponente.SelectedValue = "-1") Then
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "alert('Componente non specificato!');", True)
            Exit Sub
        End If

        ScriviDetrazioni()


        Cache(Session.Item("GRiga")) = txtRiga.Text
        Cache(Session.Item("GProgressivo")) = cmbComponente.SelectedItem.Value
        Cache(Session.Item("GLista")) = par.MiaFormat(cmbComponente.SelectedItem.Text, 30) & " " & par.MiaFormat(cmbDetrazione.SelectedItem.Text, 35) & " " & par.MiaFormat(txtImporto.Text, 8) & ",00 "



        'Response.Clear()
        'Response.Write("<script>window.close();</script>")
        'Response.End()


    End Sub

    Private Sub ScriviDetrazioni()
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            If txtOperazione.Text = "0" Then
                par.cmd.CommandText = "INSERT INTO COMP_DETRAZIONI_VSA (ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES (SEQ_COMP_DETRAZIONI_VSA.NEXTVAL," & cmbComponente.SelectedValue & "," & cmbDetrazione.SelectedValue & "," & par.VirgoleInPunti(txtImporto.Text) & ")"
                par.cmd.ExecuteNonQuery()
            Else
                par.cmd.CommandText = "SELECT * FROM COMP_DETRAZIONI_VSA WHERE ID=" & txtRiga.Text
                Dim myReaderI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderI.Read Then
                    par.cmd.CommandText = "UPDATE COMP_DETRAZIONI_VSA SET ID_COMPONENTE=" & cmbComponente.SelectedValue & ",ID_TIPO=" & cmbDetrazione.SelectedValue & ",IMPORTO=" & par.VirgoleInPunti(txtImporto.Text) & " WHERE ID=" & txtRiga.Text
                    par.cmd.ExecuteNonQuery()
                End If
                myReaderI.Close()
            End If

            salvaDetrazioni.Value = "1"

            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "CloseModal(" & salvaDetrazioni.Value & ");", True)

        Catch ex As Exception
            '*********************CHIUSURA TRANSAZIONE E CONNESSIONE**********************
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- ScriviDetrazioni" & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Public Property vIdConnessione() As String
        Get
            If Not (ViewState("par_vIdConnessione") Is Nothing) Then
                Return CStr(ViewState("par_vIdConnessione"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vIdConnessione") = value
        End Set

    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim COMPONENTE As String

        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        Response.Write("<script></script>")
        If Not IsPostBack = True Then
            par.caricaComboBox("SELECT COMP_NUCLEO_VSA.ID,COMP_NUCLEO_VSA.COGNOME||' '||NOME AS NOMINATIVO FROM COMP_NUCLEO_VSA where COMP_NUCLEO_VSA.id_DICHIARAZIONE = " & Request.QueryString("IDDICH") & " ORDER BY PROGR ASC", cmbComponente, "ID", "NOMINATIVO", True)

            par.caricaComboBox("SELECT * FROM T_TIPO_DETRAZIONI ORDER BY COD ASC", cmbDetrazione, "COD", "DESCRIZIONE", True)

            txtOperazione.Text = par.Elimina160(Request.QueryString("OP"))
            COMPONENTE = par.Elimina160(Request.QueryString("COMPONENTE"))
            Riempi(COMPONENTE)
            txtRiga.Text = par.Elimina160(Request.QueryString("RI"))

            vIdConnessione = Request.QueryString("IDCONN")

            txtImporto.Text = par.Elimina160(Request.QueryString("IM"))

            If txtOperazione.Text = "1" Then
                cmbDetrazione.SelectedIndex = -1
                If par.Elimina160(Request.QueryString("TI")) = "Spese per ricorvero in strutture so" Then
                    cmbDetrazione.Items.FindByValue("2").Selected = True
                Else
                    cmbDetrazione.Items.FindByText(par.Elimina160(Request.QueryString("TI"))).Selected = True
                End If

                cmbComponente.Items.FindByValue(par.Elimina160(Request.QueryString("COMP"))).Selected = True
            End If

        End If

        'Dim CTRL As Control
        'For Each CTRL In Me.Controls
        '    If TypeOf CTRL Is TextBox Then
        '        DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='2';document.getElementById('H1').value='0';")
        '    ElseIf TypeOf CTRL Is DropDownList Then
        '        DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='2';document.getElementById('H1').value='0';")
        '    ElseIf TypeOf CTRL Is CheckBox Then
        '        DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='2';document.getElementById('H1').value='0';")
        '    End If
        'Next
        SettaControlModifiche(Me)

    End Sub


    Private Sub SettaControlModifiche(ByVal obj As Control)
        Dim CTRL As Control
        For Each CTRL In obj.Controls
            If CTRL.Controls.Count > 0 Then
                SettaControlModifiche(CTRL)
            End If
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf CTRL Is CheckBox Then
                DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf CTRL Is CheckBoxList Then
                DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
            End If
        Next
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
End Class
