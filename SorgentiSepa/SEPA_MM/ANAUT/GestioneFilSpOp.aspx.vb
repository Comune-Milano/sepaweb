
Partial Class ANAUT_GestioneFilSpOp
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0

        If Not IsPostBack Then
            CaricaDati()
        End If
    End Sub

    Private Function CaricaDati()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim INDICEBANDO As Long = 0
            idBando.Value = INDICEBANDO

            par.cmd.CommandText = "SELECT * FROM UTENZA_BANDI WHERE stato=1 order by id desc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lblDescrBando.Text = "Stai Operando su: " & par.IfNull(myReader("descrizione"), "")
                INDICEBANDO = par.IfNull(myReader("ID"), 0)
                idBando.Value = INDICEBANDO
            Else
                lblDescrBando.Text = "Nessuna Anagrafe Utenza in corso..."
            End If
            myReader.Close()

            If INDICEBANDO <> 0 Then
                sStringaSQL_F = "SELECT UTENZA_FILIALI.ID, TAB_FILIALI.ID AS ID_STRUTTURA,TAB_FILIALI.NOME AS DESCRIZIONE,(SELECT COUNT(ID) FROM UTENZA_SPORTELLI WHERE FL_ELIMINATO=0 AND ID_FILIALE=UTENZA_FILIALI.ID) AS NUM_SPORTELLI,(SELECT COUNT(UTENZA_OPERATORI.ID) FROM UTENZA_OPERATORI,UTENZA_SPORTELLI WHERE UTENZA_OPERATORI.FL_ELIMINATO=0 AND UTENZA_SPORTELLI.ID_FILIALE=UTENZA_FILIALI.ID AND UTENZA_OPERATORI.ID_SPORTELLO=UTENZA_SPORTELLI.ID) AS NUM_OPERATORI FROM SISCOM_MI.TAB_FILIALI,UTENZA_FILIALI WHERE UTENZA_FILIALI.FL_ELIMINATO=0 AND UTENZA_FILIALI.TUTTO_PATRIMONIO=0 AND TAB_FILIALI.ID=UTENZA_FILIALI.ID_STRUTTURA AND UTENZA_FILIALI.ID_BANDO=" & INDICEBANDO & " ORDER BY TAB_FILIALI.NOME ASC"
                BindGrid_F()
            End If

            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Text = ex.Message
            lblErrore.Visible = True
        End Try
    End Function

    Private Sub BindGrid_F()
        Try

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL_F, par.OracleConn)
            Dim ds As New Data.DataSet()

            da.Fill(ds, "UTENZA_FILIALI")
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()


            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try
    End Sub

    Private Sub BindGrid_S()
        Try

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL_S, par.OracleConn)
            Dim ds As New Data.DataSet()

            da.Fill(ds, "UTENZA_SPORTELLI")
            DataGrid2.DataSource = ds
            DataGrid2.DataBind()


            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try
    End Sub

    Private Sub BindGrid_O()
        Try

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL_O, par.OracleConn)
            Dim ds As New Data.DataSet()

            da.Fill(ds, "UTENZA_OPERATORI")
            DataGrid3.DataSource = ds
            DataGrid3.DataBind()


            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try
    End Sub

    Public Property sStringaSQL_F() As String
        Get
            If Not (ViewState("par_sStringaSQL_F") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL_F"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL_F") = value
        End Set

    End Property

    Public Property sStringaSQL_S() As String
        Get
            If Not (ViewState("par_sStringaSQL_S") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL_S"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL_S") = value
        End Set

    End Property

    Public Property sStringaSQL_O() As String
        Get
            If Not (ViewState("par_sStringaSQL_O") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL_O"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL_O") = value
        End Set

    End Property

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.Cells(2).Text <> "NOME" Then
            If e.Item.ItemType = ListItemType.Item Then
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#ffffc0'}")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#eeeeee'}")
                e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('LBLIDF').value='" & e.Item.Cells(0).Text & "';document.getElementById('lbldescrizione').value='" & e.Item.Cells(2).Text & "';document.getElementById('btnCaricaSP').click();")
            Else
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#ffffc0'}")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#dcdcdc'}")
                e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('LBLIDF').value='" & e.Item.Cells(0).Text & "';document.getElementById('lbldescrizione').value='" & e.Item.Cells(2).Text & "';document.getElementById('btnCaricaSP').click();")
            End If
        End If
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid_F()
        End If
    End Sub

    Protected Sub imgNuovaFiliale_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgNuovaFiliale.Click
        If H1.Value = "1" Then
            BindGrid_F()
        End If

    End Sub

    Protected Sub DataGrid1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged

    End Sub

    Protected Sub btnCaricaSP_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCaricaSP.Click
        lblDescrBando1.Text = "SPORTELLI ASSOCIATI ALLA STRUTTURA: " & lbldescrizione.Value
        sStringaSQL_S = "SELECT replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''SceltaPatrimonio.aspx?AU=" & idBando.Value & "&IDF=" & LBLIDF.Value & "&IDS='||UTENZA_SPORTELLI.ID||''',''Dettagli'',''height=480,top=0,left=0,width=600'');£>'||'PATRIMONIO'||'</a>','$','&'),'£','" & Chr(34) & "') as  PATRIMONIO,UTENZA_SPORTELLI.*,(select count(id) from utenza_operatori where FL_ELIMINATO=0 AND id_sportello=UTENZA_SPORTELLI.ID) AS NUM_OPERATORI FROM UTENZA_SPORTELLI WHERE FL_ELIMINATO=0 AND  ID_FILIALE=" & LBLIDF.Value & " ORDER BY DESCRIZIONE ASC"
        BindGrid_S()
        LBLIDS.Value = ""

        sStringaSQL_O = "select utenza_operatori.*,'' AS GIORNI from UTENZA_OPERATORI WHERE ID=-1"
        lblDescrBando2.Text = "OPERATORI ASSOCIATI:"
        'DataGrid3.DataBind()
        BindGrid_O()
        LBLIDO.Value = ""
    End Sub

    Protected Sub DataGrid2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid2.ItemDataBound
        If e.Item.Cells(2).Text <> "NOME" Then
            If e.Item.ItemType = ListItemType.Item Then
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#ffffc0'}")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#eeeeee'}")
                e.Item.Attributes.Add("onclick", "if (Selezionato1) {Selezionato1.style.backgroundColor=''}Selezionato1=this;this.style.backgroundColor='red';document.getElementById('LBLIDS').value='" & e.Item.Cells(0).Text & "';document.getElementById('lbldescrizione1').value='" & e.Item.Cells(2).Text & "';document.getElementById('btnCaricaOP1').click();")
            Else
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#ffffc0'}")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#dcdcdc'}")
                e.Item.Attributes.Add("onclick", "if (Selezionato1) {Selezionato1.style.backgroundColor=''}Selezionato1=this;this.style.backgroundColor='red';document.getElementById('LBLIDS').value='" & e.Item.Cells(0).Text & "';document.getElementById('lbldescrizione1').value='" & e.Item.Cells(2).Text & "';document.getElementById('btnCaricaOP1').click();")
            End If
        End If
    End Sub

    Protected Sub DataGrid2_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid2.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid2.CurrentPageIndex = e.NewPageIndex
            BindGrid_S()
        End If
    End Sub

    Protected Sub DataGrid2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid2.SelectedIndexChanged

    End Sub

    Protected Sub btnEliminaFil_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaFil.Click
        If H1.Value = "1" Then
            Try
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans

                'par.cmd.CommandText = "DELETE FROM UTENZA_OPERATORI WHERE ID_SPORTELLO IN (SELECT ID FROM UTENZA_SPORTELLI WHERE ID_FILIALE=" & LBLIDF.Value & ")"
                'par.cmd.ExecuteNonQuery()

                'par.cmd.CommandText = "DELETE FROM UTENZA_SPORTELLI WHERE ID_FILIALE=" & LBLIDF.Value
                'par.cmd.ExecuteNonQuery()

                'par.cmd.CommandText = "DELETE FROM UTENZA_FILIALI WHERE ID=" & LBLIDF.Value
                'par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "UPDATE UTENZA_OPERATORI SET FL_ELIMINATO=1 WHERE ID_SPORTELLO IN (SELECT ID FROM UTENZA_SPORTELLI WHERE ID_FILIALE=" & LBLIDF.Value & ")"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "UPDATE UTENZA_SPORTELLI SET FL_ELIMINATO=1 WHERE ID_FILIALE=" & LBLIDF.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "UPDATE UTENZA_FILIALI SET FL_ELIMINATO=1 WHERE ID=" & LBLIDF.Value
                par.cmd.ExecuteNonQuery()

                par.myTrans.Commit()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                MessJQuery("Operazione effettuata!", 0, "Avviso")
                BindGrid_F()
            Catch ex As Exception
                par.myTrans.Rollback()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                lblErrore.Text = ex.Message
                lblErrore.Visible = True
            End Try
        End If
    End Sub

    Private Sub MessJQuery(ByVal Messaggio As String, ByVal Tipo As Integer, Optional ByVal Titolo As String = "Messaggio")
        Try
            Dim sc As String = ""
            If Tipo = 0 Then
                sc = ScriptErrori(Messaggio, Titolo)
            Else
                sc = ScriptChiudi(Messaggio, Titolo)
            End If
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "ScriptMsg", sc, True)
        Catch ex As Exception
            lblErrore.Text = ex.Message
            lblErrore.Visible = True
        End Try
    End Sub

    Private Function ScriptErrori(ByVal Messaggio As String, Optional ByVal Titolo As String = "Messaggio") As String
        Try
            Dim retvalue As String = ""
            Dim sb As New StringBuilder
            sb.Append("$(document).ready(function(){")
            sb.Append("$('#ScriptMsg').text('" & Messaggio & "');")
            sb.Append("$('#ScriptMsg').dialog({ autoOpen:true, modal:true, show:'blind', hide:'explode', title:'" & Titolo & "',buttons: {'Ok': function() {$(this).dialog('close');}}});")
            sb.Append("});")
            retvalue = sb.ToString()
            Return retvalue
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function ScriptChiudi(ByVal Messaggio As String, Optional ByVal Titolo As String = "Messaggio") As String
        Try
            Dim retvalue As String = ""
            Dim sb As New StringBuilder
            sb.Append("$(document).ready(function(){")
            sb.Append("$('#ScriptMsg').text('" & Messaggio & "');")
            sb.Append("$('#ScriptMsg').dialog({ autoOpen:true, modal:true, show:'blind', hide:'explode', title:'" & Titolo & "',buttons: {'Ok': function() {$(this).dialog('close');self.close();}}});")
            sb.Append("});")
            retvalue = sb.ToString()
            Return retvalue
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Protected Sub btnCaricaOP1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCaricaOP1.Click
        lblDescrBando2.Text = "OPERATORI ASSOCIATI ALLO SPORTELLO/SEDE: " & lbldescrizione1.Value
        sStringaSQL_O = "SELECT ROWNUM AS DESCRIZIONE,ID,('DAL '||TO_CHAR(TO_DATE(PERIODO_DAL,'YYYYmmdd'),'DD/MM/YYYY')||' AL '||TO_CHAR(TO_DATE(PERIODO_AL,'YYYYmmdd'),'DD/MM/YYYY')) AS PERIODO,SUBSTR(ORA_INIZIO_M,1,2)||':'||SUBSTR(ORA_INIZIO_M,3,2)||' - '||SUBSTR(ORA_FINE_M,1,2)||':'||SUBSTR(ORA_FINE_M,3,2) AS MATTINO,SUBSTR(ORA_INIZIO_P,1,2)||':'||SUBSTR(ORA_INIZIO_P,3,2)||' - '||SUBSTR(ORA_FINE_P,1,2)||':'||SUBSTR(ORA_FINE_P,3,2) AS POMERIGGIO,CASE WHEN gl_1=1 THEN 'LU/' ELSE '' END||CASE WHEN gl_1_P=1 THEN 'LU POM./' ELSE '' END||CASE WHEN gl_2=1 THEN 'MA/' ELSE '' END||CASE WHEN gl_2_P=1 THEN 'MA POM./' ELSE '' END||CASE WHEN gl_3=1 THEN 'ME/' ELSE '' END||CASE WHEN gl_3_P=1 THEN 'ME POM./' ELSE '' END||CASE WHEN gl_4=1 THEN 'GI/' ELSE '' END||CASE WHEN gl_4_P=1 THEN 'GI POM./' ELSE '' END||CASE WHEN gl_5=1 THEN 'VE/' ELSE '' END||CASE WHEN gl_5_P=1 THEN 'VE POM./' ELSE '' END||CASE WHEN gl_6=1 THEN 'SA/' ELSE '' END||CASE WHEN gl_6_P=1 THEN 'SA POM./' ELSE '' END||CASE WHEN gl_7=1 THEN 'DO/' ELSE '' END||CASE WHEN gl_7_P=1 THEN 'DO POM./' ELSE '' END AS GIORNI FROM UTENZA_OPERATORI WHERE FL_ELIMINATO=0 AND ID_SPORTELLO=" & LBLIDS.Value & " ORDER BY DESCRIZIONE ASC"
        BindGrid_O()
        LBLIDO.Value = ""
    End Sub

    Protected Sub btnEliminaSportello_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaSportello.Click
        If H2.Value = "1" Then
            Try
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans

                'par.cmd.CommandText = "DELETE FROM UTENZA_OPERATORI WHERE ID_SPORTELLO=" & LBLIDS.Value
                'par.cmd.ExecuteNonQuery()

                'par.cmd.CommandText = "DELETE FROM UTENZA_SPORTELLI WHERE ID=" & LBLIDS.Value
                'par.cmd.ExecuteNonQuery()

                'par.cmd.CommandText = "delete from UTENZA_SPORTELLI_PATRIMONIO where id_sportello=" & LBLIDS.Value
                'par.cmd.ExecuteNonQuery()

                'par.cmd.CommandText = "delete from UTENZA_LISTE_CDETT where id_sportello=" & LBLIDS.Value
                'par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "UPDATE UTENZA_OPERATORI SET FL_ELIMINATO=1 WHERE ID_SPORTELLO=" & LBLIDS.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "UPDATE UTENZA_SPORTELLI SET FL_ELIMINATO=1 WHERE ID=" & LBLIDS.Value
                par.cmd.ExecuteNonQuery()



                par.myTrans.Commit()

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                LBLIDS.Value = ""
                MessJQuery("Operazione effettuata!", 0, "Avviso")
                BindGrid_F()
                BindGrid_S()

            Catch ex As Exception
                par.myTrans.Rollback()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                lblErrore.Text = ex.Message
                lblErrore.Visible = True
            End Try
        End If
    End Sub

    Protected Sub btnEliminaOperatore_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaOperatore.Click
        If H3.Value = "1" Then
            Try
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans

                'par.cmd.CommandText = "DELETE FROM UTENZA_OPERATORI WHERE ID=" & LBLIDO.Value
                'par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "UPDATE UTENZA_OPERATORI SET FL_ELIMINATO=1 WHERE ID=" & LBLIDO.Value
                par.cmd.ExecuteNonQuery()
               
                par.myTrans.Commit()

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                MessJQuery("Operazione effettuata!", 0, "Avviso")
                BindGrid_O()

            Catch ex As Exception
                par.myTrans.Rollback()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                lblErrore.Text = ex.Message
                lblErrore.Visible = True
            End Try
        End If
    End Sub

    Protected Sub DataGrid3_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid3.ItemDataBound
        If e.Item.Cells(1).Text <> "NUMERO" Then
            If e.Item.ItemType = ListItemType.Item Then
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#ffffc0'}")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#eeeeee'}")
                e.Item.Attributes.Add("onclick", "if (Selezionato2) {Selezionato2.style.backgroundColor=''}Selezionato2=this;this.style.backgroundColor='red';document.getElementById('LBLIDO').value='" & e.Item.Cells(0).Text & "';")
            Else
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#ffffc0'}")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#dcdcdc'}")
                e.Item.Attributes.Add("onclick", "if (Selezionato2) {Selezionato2.style.backgroundColor=''}Selezionato2=this;this.style.backgroundColor='red';document.getElementById('LBLIDO').value='" & e.Item.Cells(0).Text & "';")
            End If
        End If
    End Sub

    Protected Sub DataGrid3_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid3.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid3.CurrentPageIndex = e.NewPageIndex
            BindGrid_O()
        End If
    End Sub

    Protected Sub DataGrid3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid3.SelectedIndexChanged

    End Sub

    Protected Sub imgNuovoSportello_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgNuovoSportello.Click
        If LBLIDF.Value <> "" Then
            BindGrid_F()
            BindGrid_S()
        End If
    End Sub

    Protected Sub imgNuovoOperatore_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgNuovoOperatore.Click
        If LBLIDS.Value <> "" Then
            BindGrid_O()
        End If
    End Sub

    Protected Sub imgModificaOperatore_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgModificaOperatore.Click
        If LBLIDS.Value <> "" Then
            BindGrid_O()
        End If
    End Sub
End Class
