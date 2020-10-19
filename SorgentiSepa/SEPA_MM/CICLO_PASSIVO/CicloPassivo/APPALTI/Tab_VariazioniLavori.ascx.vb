
Partial Class CICLO_PASSIVO_CicloPassivo_APPALTI_Tab_VariazioniLavori
    Inherits UserControlSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Me.txtData.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.txtData.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
            CaricaImpLavori()
            CaricaVarLavroiCanone()
            CaricaVarLavroiConsumo()

            If CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value = "1" Then
                FrmSoloLettura()
            End If

        End If
    End Sub

    Private Sub FrmSoloLettura()
        Me.btnEliminaLavCan.Visible = False
        Me.btnEliminaLavCons.Visible = False

    End Sub
    Public Sub CaricaImpLavori()
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT ID_PF_VOCE_IMPORTO AS ID_VOCE,TAB_SERVIZI.ID AS ID_SERVIZIO," _
                                & "TAB_SERVIZI.DESCRIZIONE AS SERVIZIO,(pf_voci_importo.descrizione) AS DESCRIZIONE, '' AS IMPORTO,((IMPORTO_CANONE*NVL(SCONTO_CANONE,0))/100) AS SCONTO_CANONE,((IMPORTO_CONSUMO*NVL(SCONTO_CONSUMO,0))/100) AS SCONTO_CONSUMO," _
                                & "(((IMPORTO_CANONE*NVL(SCONTO_CANONE,0))/100)-" _
                                                                                & "(SELECT NVL(SUM(IMPORTO),0) FROM SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI,SISCOM_MI.APPALTI_VARIAZIONI " _
                                                                                & " WHERE APPALTI_VARIAZIONI.ID=APPALTI_VARIAZIONI_IMPORTI.ID_VARIAZIONE AND APPALTI_VARIAZIONI.ID_TIPOLOGIA = 3" _
                                                                                & " AND ID_PF_VOCE_IMPORTO = APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO)" _
                                & " ) AS MAXCANONE," _
                                & " (((IMPORTO_CONSUMO*NVL(SCONTO_CONSUMO,0))/100)-" _
                                                                                & "(SELECT NVL(SUM(IMPORTO),0) FROM SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI,SISCOM_MI.APPALTI_VARIAZIONI" _
                                                                                & " WHERE APPALTI_VARIAZIONI.ID=APPALTI_VARIAZIONI_IMPORTI.ID_VARIAZIONE AND APPALTI_VARIAZIONI.ID_TIPOLOGIA = 4" _
                                                                                & " AND ID_PF_VOCE_IMPORTO = APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO)" _
                                & " ) AS MAXCONSUMO " _
                                & " FROM siscom_mi.appalti_lotti_servizi, siscom_mi.tab_servizi, siscom_mi.pf_voci_importo WHERE  pf_voci_importo.ID = ID_PF_VOCE_IMPORTO AND tab_servizi.ID = pf_voci_importo.ID_servizio AND id_appalto = " & CType(Me.Page, Object).vIdAppalti


            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "APPALTI_VARIAZIONI")

            Me.DataGridImportiVariaz.DataSource = ds
            DataGridImportiVariaz.DataBind()

            AddJavascriptFunction(DataGridImportiVariaz, "txtPercVariazione")

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVariazioneLavori"
        End Try
    End Sub
    Private Sub AddJavascriptFunction(ByVal Data As DataGrid, ByVal txtname As String)
        Dim i As Integer = 0
        Dim di As DataGridItem

        For i = 0 To Data.Items.Count - 1
            di = Data.Items(i)
            DirectCast(di.Cells(4).FindControl(txtname), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
        Next


    End Sub

    Protected Sub btn_ChiudiAppalti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_ChiudiAppalti.Click
        Me.txtData.Text = ""
        Me.txtNote.Text = ""
        Dim i As Integer = 0
        Dim di As DataGridItem

        For i = 0 To DataGridImportiVariaz.Items.Count - 1
            di = DataGridImportiVariaz.Items(i)
            DirectCast(di.Cells(4).FindControl("txtPercVariazione"), TextBox).Text = ""
        Next

        Me.txtAppare.Value = 0
    End Sub
    Private Sub CaricaVarLavroiCanone()
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT ID,to_char(to_date(DATA_VARIAZIONE,'yyyymmdd'),'dd/mm/yyyy') as DATA_VARIAZIONE,PROVVEDIMENTO,TRIM(TO_CHAR(PERCENTUALE,'9G999G999G999G999G990D99')) AS PERCENTUALE,TRIM(TO_CHAR(IMPORTO,'9G999G999G999G999G990D99')) AS IMPORTO FROM SISCOM_MI.APPALTI_VARIAZIONI, SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI WHERE APPALTI_VARIAZIONI.ID = APPALTI_VARIAZIONI_IMPORTI.ID_VARIAZIONE AND APPALTI_VARIAZIONI.ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti & " AND APPALTI_VARIAZIONI.ID_TIPOLOGIA = 3"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "APPALTI_VARIAZIONI")

            Me.DataGridVariazLavCanone.DataSource = ds
            DataGridVariazLavCanone.DataBind()


            'Dim tot As Double = 0
            'par.cmd.CommandText = "SELECT SUM(NVL(percentuale,0)) as PERCVAR FROM siscom_mi.appalti_variazioni_importi,siscom_mi.appalti_variazioni WHERE id_appalto = " & CType(Me.Page, Object).vIdAppalti & " AND id_tipologia = 1 AND appalti_variazioni.ID = appalti_variazioni_importi.id_variazione GROUP BY id_variazione"
            'da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            'Dim dt As New Data.DataTable
            'da.Fill(dt)
            'For Each row As Data.DataRow In dt.Rows
            '    tot = tot + (par.IfNull(row.Item("PERCVAR"), 0) / Me.DataGridImportiVariaz.Items.Count)
            'Next
            ''Me.PercUsataCanone.Value = tot



        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVariazioneLavori"
        End Try
    End Sub

    Private Sub CaricaVarLavroiConsumo()
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT ID,to_char(to_date(DATA_VARIAZIONE,'yyyymmdd'),'dd/mm/yyyy') as DATA_VARIAZIONE,PROVVEDIMENTO,TRIM(TO_CHAR(PERCENTUALE,'9G999G999G999G999G990D99')) AS PERCENTUALE,TRIM(TO_CHAR(IMPORTO,'9G999G999G999G999G990D99')) AS IMPORTO FROM SISCOM_MI.APPALTI_VARIAZIONI, SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI WHERE APPALTI_VARIAZIONI.ID = APPALTI_VARIAZIONI_IMPORTI.ID_VARIAZIONE AND APPALTI_VARIAZIONI.ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti & " AND APPALTI_VARIAZIONI.ID_TIPOLOGIA = 4"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "APPALTI_VARIAZIONI")

            Me.DataGridVariazLavConsumo.DataSource = ds
            DataGridVariazLavConsumo.DataBind()






        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVariazioneLavori"
        End Try
    End Sub

    Protected Sub btn_InserisciAppalti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_InserisciAppalti.Click
        Try

            If par.IfEmpty(Me.txtData.Text, "") <> "" Then
                If par.AggiustaData(par.IfEmpty(Me.txtData.Text, 0)) < par.AggiustaData(DirectCast(Me.Page.FindControl("txtannoinizio"), TextBox).Text) Or par.AggiustaData(Me.txtData.Text) > par.AggiustaData(DirectCast(Me.Page.FindControl("txtannofine"), TextBox).Text) Then
                    Response.Write("<SCRIPT>alert('La data della variazione deve essere compresa nel periodo di durata dell\'appalto!');</SCRIPT>")
                    Exit Sub
                End If


                Dim i As Integer = 0

                If ControllaImporti("CANONE") = True Then
                    '*******************RICHIAMO LA CONNESSIONE*********************
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)

                    '*******************RICHIAMO LA TRANSAZIONE*********************
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans
                    '*********CONTROLLO NON ESISTA GIà UNA VARIAZIONE CON LA STESSA DATA SUL CONTRATTO
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.APPALTI_VARIAZIONI WHERE ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti & " AND DATA_VARIAZIONE = " & par.AggiustaData(Me.txtData.Text) & " AND ID_TIPOLOGIA = " & txtTipo.Value
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If myReader.Read Then
                        Response.Write("<SCRIPT>alert('Attenzione...Variazione esistente su questo appalto e con la stessa data!');</SCRIPT>")
                        myReader.Close()
                        txtAppare.Value = "1"
                        Exit Sub
                    End If

                    'SELEZIONI ID APPALTI_VARIAZIONI DALLA SEQUENZA
                    Dim IdAppVariazione As Integer = 0
                    par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_APPALTI_VARIAZIONI.NEXTVAL FROM DUAL"
                    myReader = par.cmd.ExecuteReader
                    If myReader.Read Then
                        IdAppVariazione = myReader(0)
                    End If
                    myReader.Close()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_VARIAZIONI(ID,ID_APPALTO,DATA_VARIAZIONE,ID_TIPOLOGIA,PROVVEDIMENTO)" _
                                        & " VALUES(" & IdAppVariazione & ", " & CType(Me.Page, Object).vIdAppalti & ",'" & par.AggiustaData(Me.txtData.Text) & "'," & txtTipo.Value & ",'" & par.PulisciStrSql(Me.txtNote.Text.ToUpper) & "')"
                    par.cmd.ExecuteNonQuery()

                    i = 0
                    Dim di As DataGridItem
                    Dim idServizio As Integer = 0
                    Dim IdVoceServ As Integer = 0
                    Dim Percentuale As Double = 0

                    For i = 0 To Me.DataGridImportiVariaz.Items.Count - 1
                        di = Me.DataGridImportiVariaz.Items(i)
                        idServizio = di.Cells(0).Text
                        IdVoceServ = di.Cells(1).Text
                        If txtTipo.Value = 3 Then
                            If di.Cells(8).Text > 0 Then
                                Percentuale = (CDbl(par.IfEmpty(DirectCast(di.Cells(4).FindControl("txtPercVariazione"), TextBox).Text, 0)) * 100) / di.Cells(8).Text
                            Else
                                Percentuale = 0
                            End If
                        Else
                            If di.Cells(9).Text > 0 Then
                                Percentuale = (CDbl(par.IfEmpty(DirectCast(di.Cells(4).FindControl("txtPercVariazione"), TextBox).Text, 0)) * 100) / di.Cells(9).Text
                            Else
                                Percentuale = 0

                            End If

                        End If

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI (ID_VARIAZIONE,ID_PF_VOCE_IMPORTO,IMPORTO,PERCENTUALE) VALUES (" & IdAppVariazione _
                            & "," & IdVoceServ & "," & par.VirgoleInPunti(par.IfEmpty(DirectCast(di.Cells(4).FindControl("txtPercVariazione"), TextBox).Text.Replace(".", ""), 0)) & "," & par.VirgoleInPunti(Percentuale) & ")"
                        par.cmd.ExecuteNonQuery()

                        Me.txtData.Text = ""
                        Me.txtNote.Text = ""

                    Next
                    DirectCast(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1

                    CaricaImpLavori()
                    If txtTipo.Value = 3 Then
                        CaricaVarLavroiCanone()
                    Else
                        CaricaVarLavroiConsumo()
                    End If
                    txtAppare.Value = 0


                End If
            Else
                Response.Write("<SCRIPT>alert('Attenzione...La data è obbligatoria!');</SCRIPT>")

            End If
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVariazioneLavori"
        End Try

    End Sub
    Private Function ControllaImporti(ByVal tipo As String) As Boolean
        Dim i As Integer = 0
        Dim di As DataGridItem
        Dim col As Integer

        ControllaImporti = True

        If txtTipo.Value = 3 Then
            col = 6
        ElseIf txtTipo.Value = 4 Then
            col = 7
        End If

        For i = 0 To DataGridImportiVariaz.Items.Count - 1
            di = DataGridImportiVariaz.Items(i)
            If CDbl(par.IfEmpty(DirectCast(di.Cells(4).FindControl("txtPercVariazione"), TextBox).Text, 0)) > CDbl(par.IfEmpty(di.Cells(col).Text, 0)) Then

                Response.Write("<SCRIPT>alert('Attenzione...L\'importo di €. " & par.IfEmpty(DirectCast(di.Cells(4).FindControl("txtPercVariazione"), TextBox).Text, 0) & " supera il massimo consentito dalla legge pari a €. " & par.IfEmpty(di.Cells(col).Text, 0) & " !');</SCRIPT>")
                txtAppare.Value = "1"
                ControllaImporti = False
                Exit For
            End If

        Next

    End Function

    Protected Sub DataGridVariazLavCanone_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridVariazLavCanone.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         

            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow';this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_VariazioniLavori1_txtmia').value='Hai selezionato la variazione sull\'importo a CANONE del " & e.Item.Cells(1).Text.Replace("'", "\'") & " ';document.getElementById('Tab_VariazioniLavori1_id_selected').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow';this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_VariazioniLavori1_txtmia').value='Hai selezionato la variazione sull\'importo a CANONE del " & e.Item.Cells(1).Text.Replace("'", "\'") & " ';document.getElementById('Tab_VariazioniLavori1_id_selected').value='" & e.Item.Cells(0).Text & "'")
        End If

    End Sub

    Protected Sub DataGridVariazLavConsumo_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridVariazLavConsumo.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow';this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_VariazioniLavori1_txtmia').value='Hai selezionato la variazione sull\'importo a CONSUMO del " & e.Item.Cells(1).Text.Replace("'", "\'") & " ';document.getElementById('Tab_VariazioniLavori1_id_selected').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow';this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_VariazioniLavori1_txtmia').value='Hai selezionato la variazione sull\'importo a CONSUMO del " & e.Item.Cells(1).Text.Replace("'", "\'") & " ';document.getElementById('Tab_VariazioniLavori1_id_selected').value='" & e.Item.Cells(0).Text & "'")
        End If

    End Sub

    Protected Sub btnEliminaLavCan_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaLavCan.Click
        Elimina()
    End Sub
    Private Sub Elimina()
        If Me.txtElimina.Value = 1 Then
            If Me.id_selected.Value > 0 Then
                Try
                    '*******************RICHIAMO LA CONNESSIONE*********************
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    '*******************RICHIAMO LA TRANSAZIONE*********************
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans


                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.APPALTI_VARIAZIONI WHERE ID = " & id_selected.Value
                    par.cmd.ExecuteNonQuery()

                    Me.id_selected.Value = 0
                    Me.txtElimina.Value = 0
                    DirectCast(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1

                    CaricaImpLavori()
                    CaricaVarLavroiCanone()
                    CaricaVarLavroiConsumo()


                Catch ex As Exception
                    CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
                    CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVariazioneLavori"
                End Try

            End If
        End If
    End Sub

    Protected Sub btnEliminaLavCons_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaLavCons.Click
        Elimina()
    End Sub
End Class
