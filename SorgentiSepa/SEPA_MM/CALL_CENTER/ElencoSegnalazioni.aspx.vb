
Partial Class CALL_CENTER_Segnalazione
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        If IsPostBack = False Then

            If Request.QueryString("C") <> "" Then
                tipo.Value = "C"
                identificativo.Value = Request.QueryString("C")
            End If

            If Request.QueryString("E") <> "" Then
                tipo.Value = "E"
                identificativo.Value = Request.QueryString("E")
            End If

            If Request.QueryString("U") <> "" Then
                tipo.Value = "U"
                identificativo.Value = Request.QueryString("U")
            End If

            Segnalazioni()
            BindGrid()

        End If
    End Sub

    Private Function Segnalazioni()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Select Case tipo.Value
                Case "C"
                    par.cmd.CommandText = "select siscom_mi.complessi_immobiliari.*,siscom_mi.tab_filiali.* from siscom_mi.complessi_immobiliari,siscom_mi.tab_filiali where siscom_mi.complessi_immobiliari.id=" & identificativo.Value & " and " _
                        & "siscom_mi.complessi_immobiliari.id_filiale = siscom_mi.tab_filiali.id (+)"
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        lblSegnalazione.Text = "Complesso: " & par.IfNull(myReader1("cod_Complesso"), "") & " - " & par.IfNull(myReader1("denominazione") & " - Filiale: " & par.IfNull(myReader1("nome"), ""), "")
                    End If
                    myReader1.Close()
                Case "E"
                    par.cmd.CommandText = "select siscom_mi.edifici.*,siscom_mi.tab_filiali.* from siscom_mi.edifici,siscom_mi.complessi_immobiliari,siscom_mi.tab_filiali where siscom_mi.edifici.id=" & identificativo.Value & " and " _
                        & "siscom_mi.edifici.id_complesso = siscom_mi.complessi_immobiliari.id and siscom_mi.complessi_immobiliari.id_filiale = siscom_mi.tab_filiali.id (+)"
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        lblSegnalazione.Text = "Edificio: " & par.IfNull(myReader1("cod_edificio"), "") & " - " & par.IfNull(myReader1("denominazione") & " - Filiale: " & par.IfNull(myReader1("nome"), ""), "")
                    End If
                    myReader1.Close()
                Case "U"
                    par.cmd.CommandText = "select siscom_mi.unita_contrattuale.*,siscom_mi.tab_filiali.* from siscom_mi.unita_contrattuale,siscom_mi.edifici,siscom_mi.complessi_immobiliari,siscom_mi.tab_filiali " _
                        & "where siscom_mi.unita_contrattuale.id_unita =" & identificativo.Value & " and siscom_mi.unita_contrattuale.id_edificio=siscom_mi.edifici.id " _
                        & "and siscom_mi.edifici.id_complesso = siscom_mi.complessi_immobiliari.id and siscom_mi.complessi_immobiliari.id_filiale = siscom_mi.tab_filiali.id (+)"
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        lblSegnalazione.Text = "Cod. UI: " & par.IfNull(myReader1("cod_unita_immobiliare"), "") & " - Indirizzo: " & par.IfNull(myReader1("indirizzo"), "") & ", " & par.IfNull(myReader1("civico"), "") & "<br /> Filiale: " & par.IfNull(myReader1("nome"), "") & "<br /> "
                    End If
                    myReader1.Close()

            End Select


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Function

    Protected Sub Datagrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Datagrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow';}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white';}")
            e.Item.Attributes.Add("onclick", "if (selezionato) {selezionato.style.backgroundColor='';}selezionato=this;this.style.backgroundColor='red';document.getElementById('TextBox3').value='Hai selezionato " & e.Item.Cells(1).Text & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';")

        End If
    End Sub

    Protected Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            Datagrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Private Sub BindGrid()
        Try
            Dim ds As New Data.DataSet()
            Dim s As String = ""
            Dim S1 As String = "A"

            If R1.Checked = True Then
                s = ""
            End If

            If R2.Checked = True Then
                s = "TIPO_RICHIESTA=1 AND "
            End If

            If R3.Checked = True Then
                s = "TIPO_RICHIESTA=0 AND "
            End If

            'If R4.Checked = True Then
            '    s = "TIPO_RICHIESTA=2 AND "
            'End If

            'If R5.Checked = True Then
            '    s = "TIPO_RICHIESTA=3 AND "
            'End If

            If Session.Item("OP_COM") = "0" Then
                S1 = ""
            Else
                S1 = " AND SEGNALAZIONI.ORIGINE='C'"
            End If

            Select tipo.Value
                Case "C"
                    'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("select substr(segnalazioni.DESCRIZIONE_RIC,1,30)||'...' as des,DECODE(SEGNALAZIONI.TIPO_RICHIESTA,0,'GUASTI',1,'RECLAMI',2,'PROPOSTE',3,'VARIE') AS TIPO,segnalazioni.id,TO_CHAR(TO_DATE(SUBSTR(DATA_ORA_RICHIESTA,1,8),'YYYYmmdd'),'DD/MM/YYYY') AS DATA_INSERIMENTO,segnalazioni.DESCRIZIONE_RIC,tab_stati_segnalazioni.descrizione as stato FROM siscom_mi.tab_stati_segnalazioni,SISCOM_MI.SEGNALAZIONI where " & s & " tab_stati_segnalazioni.id=segnalazioni.id_stato and id_complesso=" & identificativo.Value & S1 & " ORDER BY tab_stati_segnalazioni.id desc,data_ora_richiesta desc", par.OracleConn)
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("select substr(segnalazioni.DESCRIZIONE_RIC,1,30)||'...' as des,DECODE(SEGNALAZIONI.TIPO_RICHIESTA,0,'INFORMAZIONI',1,'GUASTI') AS TIPO,segnalazioni.id,TO_CHAR(TO_DATE(SUBSTR(DATA_ORA_RICHIESTA,1,8),'YYYYmmdd'),'DD/MM/YYYY') AS DATA_INSERIMENTO,segnalazioni.DESCRIZIONE_RIC,tab_stati_segnalazioni.descrizione as stato FROM siscom_mi.tab_stati_segnalazioni,SISCOM_MI.SEGNALAZIONI where " & s & " tab_stati_segnalazioni.id=segnalazioni.id_stato and id_complesso=" & identificativo.Value & S1 & " and segnalazioni.id_unita is null ORDER BY tab_stati_segnalazioni.id desc,data_ora_richiesta desc", par.OracleConn)
                    da.Fill(ds, "SISCOM_MI.PF_MAIN")

                    Datagrid1.DataSource = ds
                    Datagrid1.DataBind()
                Case "E"
                    'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("select substr(segnalazioni.DESCRIZIONE_RIC,1,30)||'...' as des,DECODE(SEGNALAZIONI.TIPO_RICHIESTA,0,'GUASTI',1,'RECLAMI',2,'PROPOSTE',3,'VARIE') AS TIPO,segnalazioni.id,TO_CHAR(TO_DATE(SUBSTR(DATA_ORA_RICHIESTA,1,8),'YYYYmmdd'),'DD/MM/YYYY') AS DATA_INSERIMENTO,segnalazioni.DESCRIZIONE_RIC,tab_stati_segnalazioni.descrizione as stato FROM siscom_mi.tab_stati_segnalazioni,SISCOM_MI.SEGNALAZIONI where " & s & " tab_stati_segnalazioni.id=segnalazioni.id_stato and id_edificio=" & identificativo.Value & S1 & " ORDER BY tab_stati_segnalazioni.id desc,data_ora_richiesta desc", par.OracleConn)
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("select substr(segnalazioni.DESCRIZIONE_RIC,1,30)||'...' as des,DECODE(SEGNALAZIONI.TIPO_RICHIESTA,0,'INFORMAZIONI',1,'GUASTI') AS TIPO,segnalazioni.id,TO_CHAR(TO_DATE(SUBSTR(DATA_ORA_RICHIESTA,1,8),'YYYYmmdd'),'DD/MM/YYYY') AS DATA_INSERIMENTO,segnalazioni.DESCRIZIONE_RIC,tab_stati_segnalazioni.descrizione as stato FROM siscom_mi.tab_stati_segnalazioni,SISCOM_MI.SEGNALAZIONI where " & s & " tab_stati_segnalazioni.id=segnalazioni.id_stato and id_edificio=" & identificativo.Value & S1 & " and segnalazioni.id_unita is null ORDER BY tab_stati_segnalazioni.id desc,data_ora_richiesta desc", par.OracleConn)
                    da.Fill(ds, "SISCOM_MI.PF_MAIN")

                    Datagrid1.DataSource = ds
                    Datagrid1.DataBind()
                Case "U"
                    'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("select substr(segnalazioni.DESCRIZIONE_RIC,1,30)||'...' as des,DECODE(SEGNALAZIONI.TIPO_RICHIESTA,0,'GUASTI',1,'RECLAMI',2,'PROPOSTE',3,'VARIE') AS TIPO,segnalazioni.id,TO_CHAR(TO_DATE(SUBSTR(DATA_ORA_RICHIESTA,1,8),'YYYYmmdd'),'DD/MM/YYYY') AS DATA_INSERIMENTO,segnalazioni.DESCRIZIONE_RIC,tab_stati_segnalazioni.descrizione as stato FROM siscom_mi.tab_stati_segnalazioni,SISCOM_MI.SEGNALAZIONI where " & s & " tab_stati_segnalazioni.id=segnalazioni.id_stato and id_unita=" & identificativo.Value & S1 & " ORDER BY tab_stati_segnalazioni.id desc,data_ora_richiesta desc", par.OracleConn)
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("select substr(segnalazioni.DESCRIZIONE_RIC,1,30)||'...' as des,DECODE(SEGNALAZIONI.TIPO_RICHIESTA,0,'INFORMAZIONI',1,'GUASTI') AS TIPO,segnalazioni.id,TO_CHAR(TO_DATE(SUBSTR(DATA_ORA_RICHIESTA,1,8),'YYYYmmdd'),'DD/MM/YYYY') AS DATA_INSERIMENTO,segnalazioni.DESCRIZIONE_RIC,tab_stati_segnalazioni.descrizione as stato FROM siscom_mi.tab_stati_segnalazioni,SISCOM_MI.SEGNALAZIONI where " & s & " tab_stati_segnalazioni.id=segnalazioni.id_stato and id_unita=" & identificativo.Value & S1 & " ORDER BY tab_stati_segnalazioni.id desc,data_ora_richiesta desc", par.OracleConn)
                    da.Fill(ds, "SISCOM_MI.PF_MAIN")

                    Datagrid1.DataSource = ds
                    Datagrid1.DataBind()
            End Select


        Catch ex As Exception

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")


        End Try
    End Sub

    Protected Sub btnAggiorna_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAggiorna.Click
        BindGrid()
    End Sub

End Class
