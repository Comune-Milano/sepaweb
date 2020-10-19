
Partial Class ANAUT_com_patrimonio
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim COMPONENTE As String

        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If

        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)

        Response.Write("<script></script>")
        If Not IsPostBack Then
            txtABI.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            'txtCAB.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            'txtCIN.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtInter.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            'txtIBAN.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

            txtOperazione.Text = par.Elimina160(Request.QueryString("OP"))
            vIdConnessione = Request.QueryString("IDCONN")

            COMPONENTE = par.Elimina160(Request.QueryString("COMPONENTE"))
            Riempi(COMPONENTE)

            par.caricaComboBox("SELECT COMP_NUCLEO_VSA.ID,COMP_NUCLEO_VSA.COGNOME||' '||NOME AS NOMINATIVO FROM COMP_NUCLEO_VSA where COMP_NUCLEO_VSA.id_DICHIARAZIONE = " & Request.QueryString("IDDICH") & " ORDER BY PROGR ASC", cmbComponente, "ID", "NOMINATIVO", True)

            par.caricaComboBox("SELECT * FROM TIPOLOGIA_PATR_MOB ORDER BY ID ASC", cmbTipoPatrim, "ID", "DESCRIZIONE", True)


            txtRiga.Text = par.Elimina160(Request.QueryString("RI"))

            txtABI.Text = par.Elimina160(Request.QueryString("ABI"))
            'txtCAB.Text = par.Elimina160(Request.QueryString("CAB"))
            'txtCIN.Text = par.Elimina160(Request.QueryString("CIN"))
            txtInter.Text = par.Elimina160(Request.QueryString("INT"))
            txtImporto.Text = par.Elimina160(Request.QueryString("IMP"))
            'txtIBAN.Text = par.Elimina160(Request.QueryString("IBAN"))

            If txtOperazione.Text = "1" Then
                cmbComponente.Items.FindByValue(par.Elimina160(Request.QueryString("COMP"))).Selected = True

                If Request.QueryString("TIPO") <> "" Then
                    cmbTipoPatrim.Items.FindByText(Request.QueryString("TIPO")).Selected = True
                End If
            Else
                txtABI.Text = "000000000000000000000000000"
                'txtCAB.Text = "00000"
                'txtCIN.Text = "0"
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

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalva.Click
        'If txtCAB.Text = "" Then
        '    L1.Visible = True
        '    L1.Text = "(Valorizare CAB)"
        '    Exit Sub
        'Else
        '    If Len(txtCAB.Text) <> 5 Then
        '        L1.Visible = True
        '        L1.Text = "(ABI/CAB 5 caratteri)"
        '        Exit Sub
        '    Else
        '        L1.Visible = False
        '    End If
        'End If

        If txtABI.Text = "" Then
            L1.Visible = True
            L1.Text = "(Valorizare)"
            Exit Sub
        Else
            If Len(txtABI.Text) <> 27 Then
                L1.Visible = True
                L1.Text = "(IBAN 27 caratteri)"
                Exit Sub
            Else
                L1.Visible = False
            End If
        End If

        'If txtCIN.Text = "" Then
        '    L1.Visible = True
        '    L1.Text = "(Valorizare CIN)"
        '    Exit Sub
        'Else
        '    L1.Visible = False
        'End If

        ''If txtIBAN.Text = "" Then
        ''    L5.Visible = True
        ''    L5.Text = "(Valorizare)"
        ''    Exit Sub
        ''Else
        ''    L5.Visible = False
        ''End If

        If txtInter.Text = "" Then
            L5.Visible = True
        Else
            L5.Visible = False
        End If

        If IsNumeric(txtImporto.Text) = True Then
            L3.Visible = False
        Else
            L3.Visible = True
            L3.Text = "(Valore Numerico)"
            Exit Sub
        End If

        If CDbl(txtImporto.Text) >= 0 Then
            L3.Visible = False
        Else
            L3.Visible = True
            L3.Text = "(Valore superiori o uguali a 0)"
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

        If L1.Visible = True Or L5.Visible = True Or L3.Visible = True Then
            Exit Sub
        End If

        'Cache(Session.Item("GRiga")) = txtRiga.Text
        'Cache(Session.Item("GProgressivo")) = cmbComponente.SelectedItem.Value
        'Cache(Session.Item("GLista")) = par.MiaFormat(cmbComponente.SelectedItem.Text, 25) & " " & par.MiaFormat(txtABI.Text, 27) & " " & par.MiaFormat(txtInter.Text, 16) & " " & par.MiaFormat(txtImporto.Text, 8) & ",00 "

        'Response.Clear()
        'Response.Write("<script>window.close();</script>")
        'Response.End()

        If (cmbComponente.SelectedValue = "-1") Then
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "alert('Componente non specificato!');", True)
            Exit Sub
        End If

        ScriviPatrMobiliare()

    End Sub

    Private Sub ScriviPatrMobiliare()
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            Dim tipoPatrMob As String = ""

            tipoPatrMob = cmbTipoPatrim.SelectedValue

            If tipoPatrMob = "-1" Then
                tipoPatrMob = "NULL"
            End If

            If txtOperazione.Text = "0" Then
                par.cmd.CommandText = "INSERT INTO COMP_PATR_MOB_VSA (ID, ID_COMPONENTE, COD_INTERMEDIARIO, INTERMEDIARIO, IMPORTO, ID_TIPO) VALUES (SEQ_COMP_PATR_MOB_VSA.NEXTVAL," & cmbComponente.SelectedValue & ",'" & txtABI.Text & "','" & par.PulisciStrSql(txtInter.Text) & "'," & par.VirgoleInPunti(txtImporto.Text) & "," & tipoPatrMob & ")"
                par.cmd.ExecuteNonQuery()
            Else
                par.cmd.CommandText = "SELECT * FROM COMP_PATR_MOB_VSA WHERE ID=" & txtRiga.Text
                Dim myReaderI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderI.Read Then
                    par.cmd.CommandText = "UPDATE COMP_PATR_MOB_VSA SET ID_COMPONENTE=" & cmbComponente.SelectedValue & ",ID_TIPO=" & tipoPatrMob & ",IMPORTO=" & par.VirgoleInPunti(txtImporto.Text) & ",COD_INTERMEDIARIO='" & txtABI.Text & "',INTERMEDIARIO='" & par.PulisciStrSql(txtInter.Text) & "' WHERE ID=" & txtRiga.Text
                    par.cmd.ExecuteNonQuery()
                End If
                myReaderI.Close()
            End If

            salvaPatrMob.Value = "1"

            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "CloseModal(" & salvaPatrMob.Value & ");", True)

        Catch ex As Exception
            '*********************CHIUSURA TRANSAZIONE E CONNESSIONE**********************
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- ScriviPatrMob" & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub
End Class
