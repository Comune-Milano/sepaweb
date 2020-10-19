
Partial Class Condomini_Tab_Contabilita
    Inherits UserControlSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not String.IsNullOrEmpty(CType(Me.Page, Object).vIdCondominio.ToString) Then
                Cerca()
            End If
            If DirectCast(Me.Page.FindControl("ImgVisibility"), HiddenField).Value <> 1 Then
                Me.btnVisualizza.Visible = False
                Me.btnConferma.Visible = False
                Me.btnDelete.Visible = False
            End If
            Me.btnVisualizza.Attributes.Add("onclick", "Aprimodale();")
            Me.btnConferma.Attributes.Add("onclick", "NuovoGestione();")

        End If
    End Sub

    Public Sub Cerca()
        Try
            If Not String.IsNullOrEmpty(CType(Me.Page, Object).vIdCondominio.ToString) Then

                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "SELECT ID,  (to_char(to_date(DATA_INIZIO,'yyyymmdd'),'dd/mm/yyyy')||' - '||to_char(to_date(DATA_FINE,'yyyymmdd'),'dd/mm/yyyy')) AS PERIODO,(CASE WHEN STATO_BILANCIO='P0' THEN 'BOZZA' WHEN STATO_BILANCIO='P1' THEN 'CONVALIDATO' ELSE 'CONSUNTIVATO' END) AS STATO_BILANCIO,to_char(to_date(DATA_INIZIO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,to_char(to_date(DATA_FINE,'yyyymmdd'),'dd/mm/yyyy')AS DATA_FINE, (CASE WHEN TIPO = 'O' THEN 'ORDINARIA' ELSE 'STRAORDINARIA' END) AS TIPO FROM SISCOM_MI.COND_GESTIONE WHERE ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio & " ORDER BY DATA_INIZIO DESC"

                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

                Dim ds As New Data.DataSet()
                da.Fill(ds, "COND_GESTIONE")

                DataGridGestione.DataSource = ds
                DataGridGestione.DataBind()

                da.Dispose()
                ds.Dispose()
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabContabilita"
        End Try

    End Sub

    Protected Sub DataGridGestione_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridGestione.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('Tab_Contabilita1_txtmia').value='Hai selezionato la Gestione " & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('Tab_Contabilita1_txtidGest').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('Tab_Contabilita1_txtmia').value='Hai selezionato la Gestione " & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('Tab_Contabilita1_txtidGest').value='" & e.Item.Cells(0).Text & "'")
        End If
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If Me.txtidGest.Value <> 0 Then
            Cerca()
            'Response.Write("<script>window.showModalDialog('RiepGestione.aspx?IDCONDOMINIO= " & CType(Me.Page, Object).vIdCondominio() & "&IDCON=" & CType(Me.Page, Object).vIdConnessione & "&IDGEST=" & Me.txtidGest.Value & "',window, 'status:no;dialogWidth:900px;dialogHeight:480px;dialogHide:true;help:no;scroll:no');</script>")
            txtidGest.Value = 0

            'Variabile di sessione per sapere se sono state apportate modifiche e salvataggio alle finestre modali.
            If Session("MODIFYMODAL") = 1 Then
                CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1
            End If
            Session.Remove("MODIFYMODAL")
        End If

    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnDelete.Click
        Try
            If Me.txtidGest.Value <> "" And Me.txtidGest.Value <> "0" Then
                If txtConfElimina.Value = 1 Then
                    Dim convalidato As Boolean = False
                    '*******************RICHIAMO LA CONNESSIONE*********************
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    '*******************RICHIAMO LA TRANSAZIONE*********************
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans

                    'par.cmd.CommandText = "SELECT id_pagamento FROM siscom_mi.prenotazioni WHERE ID IN (SELECT ID_prenotazione FROM SISCOM_mi.cond_gestione_dett_scad WHERE id_gestione = " & txtidGest.Value & " AND id_prenotazione IS NOT NULL) AND id_pagamento IS NOT NULL"
                    'Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    'If lettore.Read Then
                    '    If par.IfNull(lettore("id_pagamento"), 0) > 0 Then
                    '        Response.Write("<script>alert('ATTENZIONE!Impossibile eliminare il bilancio!\nE\' stato già emesso il pagamento su una o più rate!');</script>")
                    '        Me.txtidGest.Value = 0
                    '        txtConfElimina.Value = 0
                    '    Else
                    '        'par.cmd.CommandText = "delete from siscom_mi.prenotazioni where id in (SELECT ID_prenotazione FROM SISCOM_mi.cond_gestione_dett_scad WHERE id_gestione = " & txtidGest.Value & " AND id_prenotazione IS NOT NULL)"
                    '        'par.cmd.ExecuteNonQuery()
                    '        par.cmd.CommandText = "DELETE FROM SISCOM_MI.COND_GESTIONE WHERE ID = " & txtidGest.Value
                    '        par.cmd.ExecuteNonQuery()
                    '        '****************MYEVENT*****************
                    '        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & CType(Me.Page, Object).vIdCondominio & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','CANCELLATO PREVENTIVO CONTABILITA CONDOMINIO')"
                    '        par.cmd.ExecuteNonQuery()

                    '        CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1

                    '        Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")
                    '        Cerca()
                    '        Me.txtidGest.Value = 0
                    '        txtConfElimina.Value = 0
                    '    End If
                    'Else

                    '    par.cmd.CommandText = "delete from siscom_mi.prenotazioni where id in (SELECT ID_prenotazione FROM SISCOM_mi.cond_gestione_dett_scad WHERE id_gestione = " & txtidGest.Value & " AND id_prenotazione IS NOT NULL)"
                    '    par.cmd.ExecuteNonQuery()
                    '    par.cmd.CommandText = "DELETE FROM SISCOM_MI.COND_GESTIONE WHERE ID = " & txtidGest.Value
                    '    par.cmd.ExecuteNonQuery()
                    '    '****************MYEVENT*****************
                    '    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & CType(Me.Page, Object).vIdCondominio & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','CANCELLATO PREVENTIVO CONTABILITA CONDOMINIO')"
                    '    par.cmd.ExecuteNonQuery()

                    '    CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1

                    '    Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")
                    '    Cerca()
                    '    Me.txtidGest.Value = 0
                    '    txtConfElimina.Value = 0
                    'End If

                    par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.PRENOTAZIONI WHERE ID IN (SELECT NVL(ID_PRENOTAZIONE,0) FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD WHERE ID_GESTIONE = " & txtidGest.Value & " AND ID_PRENOTAZIONE IS NOT NULL ) AND ID_PAGAMENTO IS NOT NULL"
                    'par.cmd.CommandText = "SELECT STATO_BILANCIO FROM SISCOM_MI.COND_GESTIONE WHERE ID = " & txtidGest.Value
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

                    If lettore.HasRows Then
                        Response.Write("<script>alert('ATTENZIONE!Non è possibile eliminare il bilancio CONVALIDATO\nperchè è stato emesso almeno un pagamento sulle prenotazioni registrate!');</script>")
                        Me.txtidGest.Value = 0
                        txtConfElimina.Value = 0
                        Exit Sub
                        'If lettore("STATO_BILANCIO") = "P1" Then
                        '    convalidato = True
                        'Else
                        '    convalidato = False
                        'End If
                    End If
                    lettore.Close()
                    'If convalidato = False Then
                    Dim prenotToDelete As String = ""
                    par.cmd.CommandText = "SELECT DISTINCT ID_PRENOTAZIONE FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD WHERE ID_GESTIONE = " & txtidGest.Value & " AND ID_PRENOTAZIONE IS NOT NULL "
                    lettore = par.cmd.ExecuteReader
                    While lettore.Read
                        prenotToDelete = prenotToDelete & par.IfNull(lettore("ID_PRENOTAZIONE"), 0) & ","
                    End While
                    lettore.Close()

                    'par.cmd.CommandText = "DELETE FROM SISCOM_MI.PRENOTAZIONI WHERE ID IN (SELECT DISTINCT ID_PRENOTAZIONE FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD WHERE ID_GESTIONE = " & txtidGest.Value & " AND ID_PRENOTAZIONE IS NOT NULL )"
                    'par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.COND_GESTIONE WHERE ID = " & txtidGest.Value
                    par.cmd.ExecuteNonQuery()

                    If Not String.IsNullOrEmpty(prenotToDelete) Then
                        prenotToDelete = prenotToDelete.Substring(0, prenotToDelete.LastIndexOf(","))
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.PRENOTAZIONI WHERE ID IN (" & prenotToDelete & ")"
                        par.cmd.ExecuteNonQuery()

                    End If
                    '****************MYEVENT*****************
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & CType(Me.Page, Object).vIdCondominio & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','CANCELLATO PREVENTIVO CONTABILITA CONDOMINIO')"
                    par.cmd.ExecuteNonQuery()

                    CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1

                    Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")
                    Cerca()
                    Me.txtidGest.Value = 0
                    txtConfElimina.Value = 0
                    'Else
                    '    Response.Write("<script>alert('ATTENZIONE!Non è possibile eliminare un bilancio CONVALIDATO!');</script>")
                    '    Me.txtidGest.Value = 0
                    '    txtConfElimina.Value = 0
                    'End If

                Else
                    Me.txtidGest.Value = 0
                    txtConfElimina.Value = 0
                    Response.Write("<script>alert('Operazione Annullata!');</script>")

                End If
            Else
                Response.Write("<script>alert('Selezionare un elemento da eliminare!');</script>")

            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabContabilita"
        End Try

    End Sub


    Protected Sub btnConferma_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnConferma.Click
        Cerca()
        Me.txtAnnoInizio.Text = ""
        'Variabile di sessione per sapere se sono state apportate modifiche e salvataggio alle finestre modali.
        If Session("MODIFYMODAL") = 1 Then
            CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1
        End If
        Session.Remove("MODIFYMODAL")
    End Sub


End Class
