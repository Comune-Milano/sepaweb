
Partial Class GestioneAutonoma_Tab_Servizi
    Inherits UserControlSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CaricaServizi()
            CaricaDati()
            Me.txtImporto.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
            Me.txtImpFinanz.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
            Me.txtImporto.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")
            Me.txtImpFinanz.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")

        End If
    End Sub
    Public Sub CaricaServizi()
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans
            Me.cmbservizio.Items.Clear()
            par.cmd.CommandText = "select id, descrizione from siscom_mi.autogestioni_tab_servizi where id not in (select id_servizio from siscom_mi.autogestioni_servizi where id_esercizio =" & par.IfEmpty(CType(Me.Page, Object).vIdEsercizio, "-1") & ") order by id asc"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                cmbservizio.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()


        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabServizi - CaricaServizi -" & ex.Message
        End Try

    End Sub
    Public Sub CaricaDati()
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT ID_ESERCIZIO, ID_SERVIZIO,SUBSTR(AUTOGESTIONI_TAB_SERVIZI.DESCRIZIONE,0,8)AS DESCRIZIONE, TRIM(TO_CHAR(IMPORTO,'9G999G999G990D99'))AS IMPORTO, TRIM(TO_CHAR(IMPORTO_DA_FINANZIARE,'9G999G999G990D99'))AS IMPORTO_DA_FINANZIARE, (CASE WHEN FREQUENZA_PAGAMENTO=1 THEN 'MENSILE' WHEN FREQUENZA_PAGAMENTO=2 THEN 'BIMESTRALE'  WHEN FREQUENZA_PAGAMENTO=3 THEN 'TRIMESTRALE' WHEN FREQUENZA_PAGAMENTO=4 THEN 'QUADRIMESTRALE' WHEN FREQUENZA_PAGAMENTO=6 THEN 'SEMESTRALE' WHEN FREQUENZA_PAGAMENTO=12 THEN 'ANNUALE' ELSE 'NON DEFINITO' END) AS FREQUENZA_PAGAMENTO" _
                                & " FROM SISCOM_MI.AUTOGESTIONI_SERVIZI, SISCOM_MI.AUTOGESTIONI_TAB_SERVIZI WHERE ID_SERVIZIO = AUTOGESTIONI_TAB_SERVIZI.ID AND ID_ESERCIZIO = " & par.IfEmpty(CType(Me.Page, Object).vIdEsercizio, "-1")

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable()
            da.Fill(dt)

            DataGridServizi.DataSource = dt
            DataGridServizi.DataBind()

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabServizi - CaricaDati -" & ex.Message
        End Try

    End Sub
    Protected Sub btnAggiorna_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAggiorna.Click
        Try
            If String.IsNullOrEmpty(Me.txtImporto.Text) = False AndAlso String.IsNullOrEmpty(Me.txtImpFinanz.Text) = False Then

                If CDbl(Me.txtImporto.Text.Replace(".", "")) < CDbl(Me.txtImpFinanz.Text.Replace(".", "")) Then

                    '*******************RICHIAMO LA CONNESSIONE*********************
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    '*******************RICHIAMO LA TRANSAZIONE*********************
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans

                    If Me.cmbservizio.SelectedValue = 4 Then
                        Dim conta As Integer = 0
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.AUTOGESTIONI_SERVIZI WHERE ID_ESERCIZIO = " & CType(Me.Page, Object).vIdEsercizio & " AND ID_SERVIZIO = 2 OR ID_SERVIZIO = 3"
                        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReader1.Read
                            conta = conta + 1
                        End While
                        myReader1.Close()

                        If conta < 2 Then
                            Response.Write("<script>alert('Per inserire il servizio del GRUPPO 4 è obbligatorio inserire prima i servizi del GRUPPO 2 e GRUPPO 3!');</script>")
                            Me.txtVisServ.Value = 2
                            Pulisci()
                            CaricaDati()
                            CaricaServizi()
                            Exit Sub
                        End If
                    End If

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.AUTOGESTIONI_SERVIZI ( ID_ESERCIZIO, ID_SERVIZIO, IMPORTO, IMPORTO_DA_FINANZIARE,FREQUENZA_PAGAMENTO ) VALUES" _
                                        & " ( " & CType(Me.Page, Object).vIdEsercizio & ", " & Me.cmbservizio.SelectedValue & ", " & par.VirgoleInPunti(Me.txtImporto.Text.Replace(".", "")) _
                                        & ", " & par.VirgoleInPunti(Me.txtImpFinanz.Text.Replace(".", "")) & ", " & Me.cmbFrequenza.SelectedValue & ") "
                    par.cmd.ExecuteNonQuery()
                    '**********CHIAMO LA SCRITTURA DELL'EVENTO 
                    CType(Me.Page, Object).WriteEvent("F02", "AGGIUNTA DEL " & Me.cmbservizio.SelectedItem.Text.Substring(0, 8))

                    Pulisci()
                    CaricaDati()
                    CaricaServizi()
                    Me.txtVisServ.Value = 2
                Else
                    Me.txtVisServ.Value = 2
                    Response.Write("<script>alert('ATTENZIONE!L\'importo da finanziare deve essere superiore dell\'importo richiesto!');</script>")

                End If
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabServizi - BtnAggiorna -" & ex.Message

        End Try

    End Sub
    Private Sub Pulisci()
        Me.txtImporto.Text = ""
        Me.txtImpFinanz.Text = ""
        Me.cmbFrequenza.SelectedValue = 0
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnDelete.Click
        Try

            If Me.txtSelected.Value > 0 Then
                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "DELETE FROM SISCOM_MI.AUTOGESTIONI_SERVIZI WHERE ID_SERVIZIO = " & Me.txtSelected.Value & " AND ID_ESERCIZIO = " & CType(Me.Page, Object).vIdEsercizio
                par.cmd.ExecuteNonQuery()
                Pulisci()
                CaricaDati()
                CaricaServizi()
                '**********CHIAMO LA SCRITTURA DELL'EVENTO 
                CType(Me.Page, Object).WriteEvent("F56", "CANCELLAZIONE DEL GRUPPO SERVIZI COD." & Me.txtSelected.Value)
                Me.txtSelected.Value = 0

            End If
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabServizi - btnDelete -" & ex.Message

        End Try


    End Sub

    Protected Sub DataGridServizi_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridServizi.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Servizi1$txtmia').value='Hai selezionato: " & e.Item.Cells(2).Text.Replace("'", "\'") & "';document.getElementById('Tab_Servizi1$txtSelected').value='" & e.Item.Cells(1).Text & "';")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Servizi1$txtmia').value='Hai selezionato: " & e.Item.Cells(2).Text.Replace("'", "\'") & "';document.getElementById('Tab_Servizi1$txtSelected').value='" & e.Item.Cells(1).Text & "';")
        End If

    End Sub
End Class
