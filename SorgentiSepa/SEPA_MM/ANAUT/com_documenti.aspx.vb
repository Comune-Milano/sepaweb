
Partial Class ANAUT_com_documenti
    Inherits PageSetIdMode
    Dim par As New CM.Global

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
            vIdConnessione = Request.QueryString("IDCONN")
            par.caricaComboBox("SELECT UTENZA_COMP_NUCLEO.ID,UTENZA_COMP_NUCLEO.COGNOME||' '||NOME AS NOMINATIVO FROM UTENZA_COMP_NUCLEO where UTENZA_COMP_NUCLEO.id_DICHIARAZIONE = " & Request.QueryString("IDDICH") & " ORDER BY PROGR ASC", cmbComponente, "ID", "NOMINATIVO", True)

            txtOperazione.Text = par.Elimina160(Request.QueryString("OP"))
            COMPONENTE = par.Elimina160(Request.QueryString("COMPONENTE"))
            Riempi(COMPONENTE)
            RiempiTipologia()



        End If

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

    Private Function RiempiTipologia()
        Try
            par.OracleConn.Open()

            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT * FROM UTENZA_DOC_NECESSARI where id_bando_au =" & Session.Item("idBandoAU") & " ORDER BY DESCRIZIONE ASC"
            Dim myRec55 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            Do While myRec55.Read
                cmbTipo.Items.Add(New ListItem(myRec55("DESCRIZIONE"), myRec55("ID")))
            Loop
            myRec55.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Function

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

        cmbComponente.Items.Add(New ListItem("--", "-1"))
        cmbTipo.ClearSelection()
        cmbComponente.Items.FindByText("--").Selected = True
    End Function

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalva.Click
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)

            Dim esistente As Boolean = False

            par.cmd.CommandText = "SELECT * FROM UTENZA_DOC_MANCANTE WHERE ID_DICHIARAZIONE=" & Request.QueryString("IDDICH") & " AND ID_DOC= " & cmbTipo.SelectedValue
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader.Read
                If LTrim(RTrim(par.IfNull(myReader("DESCRIZIONE"), ""))) = LTrim(RTrim(Replace(cmbTipo.SelectedItem.Text & " del Sig./Sig.ra " & cmbComponente.SelectedItem.Text & " " & txtDescr.Text, vbCrLf, " "))) Then
                    esistente = True
                End If
            Loop
            myReader.Close()

            If esistente = False Then
                par.cmd.CommandText = "INSERT INTO UTENZA_DOC_MANCANTE (ID_DICHIARAZIONE, ID_DOC, DESCRIZIONE," _
                                    & " ID_COMP_DOC_MANC,NOTE_DOC_MANC) VALUES (" & Request.QueryString("IDDICH") & "," & cmbTipo.SelectedValue & ",'" & Replace(par.PulisciStrSql(cmbTipo.SelectedItem.Text & " del Sig./Sig.ra " & cmbComponente.SelectedItem.Text & " " & txtDescr.Text), vbCrLf, " ") & "'," _
                                    & "" & par.RitornaNullSeMenoUno(Me.cmbComponente.SelectedValue) & ",'" & par.PulisciStrSql(Me.txtDescr.Text) & "')"
                par.cmd.ExecuteNonQuery()
                If Not IsNothing(par.myTrans) Then
                    par.myTrans.Commit()
                End If
                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
                salvaDocumenti.Value = "1"
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "CloseModal(" & salvaDocumenti.Value & ");", True)
            Else
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "alert('Documento già inserito!');", True)
            End If
        Catch ex As Exception
            '*********************CHIUSURA TRANSAZIONE E CONNESSIONE**********************
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- ScriviDocumenti" & ex.Message)
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






End Class
