
Partial Class CALL_CENTER_ElencoImpiantiUI
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If IsPostBack = False Then
            identificativo.Value = Request.QueryString("ID")
            tipo.Value = Request.QueryString("T")
            If tipo.Value = "U" Then
                linkCodiceUI()
            End If
            Impianti()
        End If

    End Sub

    Protected Sub Impianti()

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim dt As New Data.DataTable
            Dim RIGA As System.Data.DataRow

            dt.Columns.Add("COD_IMPIANTO", Type.GetType("System.String"))
            dt.Columns.Add("DESCRIZIONE", Type.GetType("System.String"))
            dt.Columns.Add("TIPOLOGIA", Type.GetType("System.String"))
            dt.Columns.Add("RIFERIMENTO", Type.GetType("System.String"))
            dt.Columns.Add("ID_IMPIANTO", Type.GetType("System.String"))

            Select tipo.Value
                Case "U" 'Impianti che si riferiscono ad una segnalazione effettuata da un inquilino

                    'CENTRALI TERMICHE (IMPIANTI_UI.ID_UNITA) 
                    par.cmd.CommandText = "SELECT DISTINCT (SISCOM_MI.IMPIANTI.COD_IMPIANTO),SISCOM_MI.IMPIANTI.ID AS ID_IMPIANTO,SISCOM_MI.IMPIANTI.DESCRIZIONE,SISCOM_MI.TIPOLOGIA_IMPIANTI.DESCRIZIONE AS TIPOLOGIA " _
                    & "FROM SISCOM_MI.IMPIANTI_UI, SISCOM_MI.IMPIANTI, SISCOM_MI.TIPOLOGIA_IMPIANTI WHERE SISCOM_MI.IMPIANTI_UI.ID_IMPIANTO = SISCOM_MI.IMPIANTI.ID AND SISCOM_MI.IMPIANTI.COD_TIPOLOGIA =" _
                    & "SISCOM_MI.TIPOLOGIA_IMPIANTI.COD AND SISCOM_MI.IMPIANTI_UI.ID_UNITA=" & identificativo.Value & " AND SISCOM_MI.TIPOLOGIA_IMPIANTI.COD <> 'SO' ORDER BY SISCOM_MI.IMPIANTI.DESCRIZIONE ASC"

                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader.Read
                        RIGA = dt.NewRow()
                        RIGA.Item("COD_IMPIANTO") = par.IfNull(myReader("COD_IMPIANTO"), "")
                        RIGA.Item("DESCRIZIONE") = par.IfNull(myReader("DESCRIZIONE"), "")
                        RIGA.Item("TIPOLOGIA") = par.IfNull(myReader("TIPOLOGIA"), "")
                        RIGA.Item("ID_IMPIANTO") = par.IfNull(myReader("ID_IMPIANTO"), "")
                        RIGA.Item("RIFERIMENTO") = "CENTRALE TERMICA (UNITA' IMMOBILIARE)"
                        dt.Rows.Add(RIGA)
                    End While

                    myReader.Close()

                    'CALDAIETTE (IMPIANTI.ID_UNITA_IMMOBILIARE) 
                    par.cmd.CommandText = "SELECT DISTINCT (SISCOM_MI.IMPIANTI.COD_IMPIANTO),SISCOM_MI.IMPIANTI.ID AS ID_IMPIANTO,SISCOM_MI.IMPIANTI.DESCRIZIONE,SISCOM_MI.TIPOLOGIA_IMPIANTI.DESCRIZIONE AS TIPOLOGIA " _
                        & "FROM SISCOM_MI.IMPIANTI,SISCOM_MI.TIPOLOGIA_IMPIANTI WHERE SISCOM_MI.IMPIANTI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_IMPIANTI.COD AND SISCOM_MI.IMPIANTI.ID_UNITA_IMMOBILIARE = " & identificativo.Value & ""
                    myReader = par.cmd.ExecuteReader()

                    While myReader.Read
                        RIGA = dt.NewRow()
                        RIGA.Item("COD_IMPIANTO") = par.IfNull(myReader("COD_IMPIANTO"), "")
                        RIGA.Item("DESCRIZIONE") = par.IfNull(myReader("DESCRIZIONE"), "")
                        RIGA.Item("TIPOLOGIA") = par.IfNull(myReader("TIPOLOGIA"), "")
                        RIGA.Item("ID_IMPIANTO") = par.IfNull(myReader("ID_IMPIANTO"), "")
                        RIGA.Item("RIFERIMENTO") = "UNITA' IMMOBILIARE"
                        dt.Rows.Add(RIGA)
                    End While

                    myReader.Close()

                    'EDIFICI
                    par.cmd.CommandText = "SELECT DISTINCT (SISCOM_MI.IMPIANTI.COD_IMPIANTO), SISCOM_MI.IMPIANTI.ID AS ID_IMPIANTO,SISCOM_MI.IMPIANTI.DESCRIZIONE,SISCOM_MI.TIPOLOGIA_IMPIANTI.DESCRIZIONE AS TIPOLOGIA " _
                        & "FROM SISCOM_MI.IMPIANTI, SISCOM_MI.TIPOLOGIA_IMPIANTI WHERE SISCOM_MI.IMPIANTI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_IMPIANTI.COD AND SISCOM_MI.IMPIANTI.ID_EDIFICIO IN " _
                        & "(SELECT SISCOM_MI.EDIFICI.ID AS ID_EDIFICIO FROM SISCOM_MI.EDIFICI, SISCOM_MI.UNITA_IMMOBILIARI WHERE SISCOM_MI.EDIFICI.ID = SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO " _
                        & "AND SISCOM_MI.UNITA_IMMOBILIARI.ID=" & identificativo.Value & ") AND SISCOM_MI.TIPOLOGIA_IMPIANTI.COD <> 'SO' ORDER BY SISCOM_MI.IMPIANTI.DESCRIZIONE ASC"
                    myReader = par.cmd.ExecuteReader()

                    While myReader.Read
                        RIGA = dt.NewRow()
                        RIGA.Item("COD_IMPIANTO") = par.IfNull(myReader("COD_IMPIANTO"), "")
                        RIGA.Item("DESCRIZIONE") = par.IfNull(myReader("DESCRIZIONE"), "")
                        RIGA.Item("TIPOLOGIA") = par.IfNull(myReader("TIPOLOGIA"), "")
                        RIGA.Item("ID_IMPIANTO") = par.IfNull(myReader("ID_IMPIANTO"), "")
                        RIGA.Item("RIFERIMENTO") = "EDIFICIO"
                        dt.Rows.Add(RIGA)
                    End While

                    myReader.Close()

                    'COMPLESSI
                    par.cmd.CommandText = "SELECT DISTINCT (SISCOM_MI.IMPIANTI.COD_IMPIANTO),SISCOM_MI.IMPIANTI.ID AS ID_IMPIANTO,SISCOM_MI.IMPIANTI.DESCRIZIONE,SISCOM_MI.TIPOLOGIA_IMPIANTI.DESCRIZIONE AS TIPOLOGIA " _
                        & "FROM SISCOM_MI.IMPIANTI, SISCOM_MI.TIPOLOGIA_IMPIANTI, SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.EDIFICI, SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                    & "WHERE SISCOM_MI.EDIFICI.ID = SISCOM_MI.UNITA_CONTRATTUALE.ID_EDIFICIO AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID = SISCOM_MI.EDIFICI.ID_COMPLESSO " _
                    & "AND SISCOM_MI.EDIFICI.ID_COMPLESSO = SISCOM_MI.IMPIANTI.ID_COMPLESSO AND SISCOM_MI.TIPOLOGIA_IMPIANTI.COD = SISCOM_MI.IMPIANTI.COD_TIPOLOGIA " _
                    & "AND SISCOM_MI.UNITA_CONTRATTUALE.ID_UNITA =" & identificativo.Value & " AND SISCOM_MI.TIPOLOGIA_IMPIANTI.COD <> 'SO' AND SISCOM_MI.IMPIANTI.ID_EDIFICIO " _
                    & "IS NULL ORDER BY SISCOM_MI.IMPIANTI.DESCRIZIONE ASC"
                    myReader = par.cmd.ExecuteReader()

                    While myReader.Read
                        RIGA = dt.NewRow()
                        RIGA.Item("COD_IMPIANTO") = par.IfNull(myReader("COD_IMPIANTO"), "")
                        RIGA.Item("DESCRIZIONE") = par.IfNull(myReader("DESCRIZIONE"), "")
                        RIGA.Item("TIPOLOGIA") = par.IfNull(myReader("TIPOLOGIA"), "")
                        RIGA.Item("ID_IMPIANTO") = par.IfNull(myReader("ID_IMPIANTO"), "")
                        RIGA.Item("RIFERIMENTO") = "COMPLESSO"
                        dt.Rows.Add(RIGA)
                    End While

                    myReader.Close()

                    'SCALA (ASCENSORI)
                    par.cmd.CommandText = "SELECT DISTINCT (SISCOM_MI.IMPIANTI.COD_IMPIANTO),SISCOM_MI.IMPIANTI.ID AS ID_IMPIANTO,SISCOM_MI.IMPIANTI.DESCRIZIONE,SISCOM_MI.TIPOLOGIA_IMPIANTI.DESCRIZIONE AS TIPOLOGIA " _
                        & "FROM SISCOM_MI.IMPIANTI, SISCOM_MI.IMPIANTI_SCALE, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.TIPOLOGIA_IMPIANTI WHERE UNITA_IMMOBILIARI.ID = " & identificativo.Value & " AND " _
                        & "IMPIANTI.COD_TIPOLOGIA='SO' AND SISCOM_MI.TIPOLOGIA_IMPIANTI.COD = SISCOM_MI.IMPIANTI.COD_TIPOLOGIA AND IMPIANTI_SCALE.ID_SCALA = UNITA_IMMOBILIARI.ID_SCALA AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO"
                    myReader = par.cmd.ExecuteReader()

                    While myReader.Read
                        RIGA = dt.NewRow()
                        RIGA.Item("COD_IMPIANTO") = par.IfNull(myReader("COD_IMPIANTO"), "")
                        RIGA.Item("DESCRIZIONE") = par.IfNull(myReader("DESCRIZIONE"), "")
                        RIGA.Item("TIPOLOGIA") = par.IfNull(myReader("TIPOLOGIA"), "")
                        RIGA.Item("ID_IMPIANTO") = par.IfNull(myReader("ID_IMPIANTO"), "")
                        RIGA.Item("RIFERIMENTO") = "SCALA"
                        dt.Rows.Add(RIGA)
                    End While

                    myReader.Close()

                Case "C" 'Impianti che si riferiscono ad una segnalazione effettuata a livello di complesso
                    par.cmd.CommandText = "SELECT SISCOM_MI.IMPIANTI.COD_IMPIANTO, SISCOM_MI.IMPIANTI.ID AS ID_IMPIANTO,SISCOM_MI.IMPIANTI.DESCRIZIONE,SISCOM_MI.TIPOLOGIA_IMPIANTI.DESCRIZIONE AS TIPOLOGIA " _
                                        & "FROM SISCOM_MI.IMPIANTI, SISCOM_MI.TIPOLOGIA_IMPIANTI WHERE SISCOM_MI.IMPIANTI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_IMPIANTI.COD AND SISCOM_MI.IMPIANTI.ID_COMPLESSO=" _
                                        & identificativo.Value & " ORDER BY SISCOM_MI.IMPIANTI.DESCRIZIONE ASC"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                    While myReader.Read
                        RIGA = dt.NewRow()
                        RIGA.Item("COD_IMPIANTO") = par.IfNull(myReader("COD_IMPIANTO"), "")
                        RIGA.Item("DESCRIZIONE") = par.IfNull(myReader("DESCRIZIONE"), "")
                        RIGA.Item("TIPOLOGIA") = par.IfNull(myReader("TIPOLOGIA"), "")
                        RIGA.Item("ID_IMPIANTO") = par.IfNull(myReader("ID_IMPIANTO"), "")
                        RIGA.Item("RIFERIMENTO") = "COMPLESSO"
                        dt.Rows.Add(RIGA)
                    End While

                    myReader.Close()

                Case "E" 'Impianti che si riferiscono ad una segnalazione effettuata a livello di edificio

                    par.cmd.CommandText = "SELECT DISTINCT (SISCOM_MI.IMPIANTI.COD_IMPIANTO), SISCOM_MI.IMPIANTI.ID AS ID_IMPIANTO,SISCOM_MI.IMPIANTI.DESCRIZIONE,SISCOM_MI.IMPIANTI.ID_EDIFICIO,SISCOM_MI.TIPOLOGIA_IMPIANTI.DESCRIZIONE AS TIPOLOGIA " _
                    & "FROM SISCOM_MI.IMPIANTI, SISCOM_MI.TIPOLOGIA_IMPIANTI, SISCOM_MI.EDIFICI WHERE SISCOM_MI.IMPIANTI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_IMPIANTI.COD AND SISCOM_MI.IMPIANTI.ID_COMPLESSO IN " _
                    & "(SELECT SISCOM_MI.EDIFICI.ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE SISCOM_MI.EDIFICI.ID = " & identificativo.Value & ") ORDER BY SISCOM_MI.IMPIANTI.DESCRIZIONE ASC"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                    While myReader.Read
                        RIGA = dt.NewRow()
                        RIGA.Item("COD_IMPIANTO") = par.IfNull(myReader("COD_IMPIANTO"), "")
                        RIGA.Item("DESCRIZIONE") = par.IfNull(myReader("DESCRIZIONE"), "")
                        RIGA.Item("TIPOLOGIA") = par.IfNull(myReader("TIPOLOGIA"), "")
                        RIGA.Item("ID_IMPIANTO") = par.IfNull(myReader("ID_IMPIANTO"), "")
                        If myReader("ID_EDIFICIO").ToString = identificativo.Value Then
                            RIGA.Item("RIFERIMENTO") = "EDIFICIO"
                        Else
                            RIGA.Item("RIFERIMENTO") = "COMPLESSO"
                        End If

                        dt.Rows.Add(RIGA)
                    End While

                    myReader.Close()

            End Select
            Datagrid1.DataSource = dt
            Datagrid1.DataBind()

            Dim conta As Integer = 0
            conta = dt.Rows.Count
            If Not conta = 0 Then
                lblNumImpianti.Text = "Numero impianti: " & conta
            Else
                lblNumImpianti.Text = "Non ci sono impianti per questa unità"
                Response.Write("<script>alert('Nessun impianto trovato per questa unità!');self.close();</script>")
            End If

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()



        Catch ex As Exception
            Response.Write(ex.Message)
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try

    End Sub

    Protected Sub linkCodiceUI()

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            par.cmd.CommandText = "select id,cod_unita_immobiliare as codice,'Unità Cod.'||cod_unita_immobiliare as titolo from siscom_mi.unita_immobiliari where id=" & identificativo.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lblIDU.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&ID=" & par.IfNull(myReader("codice"), "") & "','Dettagli','height=580,top=0,left=0,width=780');" & Chr(34) & ">" & par.IfNull(myReader("titolo"), "") & "</a>"
            End If
            myReader.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try

    End Sub

    Protected Sub Datagrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Datagrid1.ItemDataBound

        If e.Item.ItemType = ListItemType.Item Then  
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow';}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white';}")
            e.Item.Attributes.Add("onclick", "if (selezionato) {selezionato.style.backgroundColor='';}selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmsg').value='Hai selezionato l\'impianto: " & e.Item.Cells(0).Text & "';")

        End If

        If e.Item.ItemType = ListItemType.AlternatingItem Then  
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow';}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro';}")
            e.Item.Attributes.Add("onclick", "if (selezionato) {selezionato.style.backgroundColor='';}selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmsg').value='Hai selezionato l\'impianto: " & e.Item.Cells(0).Text & "';")
        End If

    End Sub

    'Protected Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged

    '    If e.NewPageIndex >= 0 Then
    '        Datagrid1.CurrentPageIndex = e.NewPageIndex
    '        Impianti()
    '    End If
    'End Sub

End Class
