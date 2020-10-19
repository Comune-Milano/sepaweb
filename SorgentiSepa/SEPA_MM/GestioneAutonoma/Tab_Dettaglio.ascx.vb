
Partial Class GestioneAutonoma_Tab_Dettaglio
    Inherits UserControlSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            CaricaDati()
        End If


    End Sub
    Public Sub CaricaElencoInquilini()
        Try
            If CType(Me.Page, Object).vSelezionati <> "" Then

                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans


                par.cmd.CommandText = "SELECT DISTINCT ID_ANAGRAFICA,(CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END) AS NOMINATIVO," _
                & "(CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN PARTITA_IVA  ELSE COD_FISCALE END) AS CF_PIVA," _
                & "(INDIRIZZO_RESIDENZA ||'  '|| CIVICO_RESIDENZA||', '|| CAP_RESIDENZA||' '||COMUNE_RESIDENZA||'('||PROVINCIA_RESIDENZA||')')AS INDIRIZZO " _
                & "FROM SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.ANAGRAFICA " _
                & "WHERE SOGGETTI_CONTRATTUALI.ID_CONTRATTO = UNITA_CONTRATTUALE.ID_CONTRATTO And ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA " _
                & "AND UNITA_CONTRATTUALE.ID_EDIFICIO IN(" & CType(Me.Page, Object).vSelezionati & ") "

                par.cmd.CommandText = par.cmd.CommandText & " ORDER BY NOMINATIVO ASC "
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable()
                da.Fill(dt)
                DataGridElencoInquilini.DataSource = dt
                DataGridElencoInquilini.DataBind()
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabDettagglio - CaricaElencoInquilini -" & ex.Message
        End Try
    End Sub
    Public Sub AggiungiComitato()
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            Dim conta As Integer = 0
            Dim i As Integer = 0
            Dim di As DataGridItem
            Dim idAnag As String = ""

            If Me.DataGridElencoInquilini.Items.Count > 0 Then
                For i = 0 To Me.DataGridElencoInquilini.Items.Count - 1
                    di = Me.DataGridElencoInquilini.Items(i)
                    idAnag = di.Cells(0).Text
                    If DirectCast(di.Cells(1).FindControl("ChkSelezione"), CheckBox).Checked = True Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.AUTOGESTIONI_COMITATO " _
                                            & " (ID_ESERCIZIO,ID_ANAGRAFICA) VALUES (" & CType(Me.Page, Object).vIdEsercizio & "," & idAnag & ")"
                        par.cmd.ExecuteNonQuery()
                        conta = conta + 1
                    End If
                Next
            End If
            CType(Me.Page, Object).vNumComitato = conta

            '**********CHIAMO LA SCRITTURA DELL'EVENTO 
            CType(Me.Page, Object).WriteEvent("F02", "COMPOSIZIONE DEL COMITATO")


            BindGrid()
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabDettagglio - AggiungiComitato -" & ex.Message
        End Try

    End Sub

    Protected Sub Aggiungi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Aggiungi.Click
        AggiungiComitato()
    End Sub
    Public Sub CaricaDati()
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans


            If CType(Me.Page, Object).vIdGestAutonoma() <> "" Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.AUTOGESTIONI_ESERCIZI WHERE ID_AUTOGESTIONE = " & CType(Me.Page, Object).vIdGestAutonoma() _
                    & " AND ID_ESERCIZIO = " & par.IfEmpty(DirectCast(Me.Page.FindControl("cmbAnno"), DropDownList).SelectedValue, "-1")
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If myReader.Read Then
                    Me.txtCognome.Text = par.IfNull(myReader("RAPP_COGNOME"), "")
                    Me.txtnome.Text = par.IfNull(myReader("RAPP_NOME"), "")
                    Me.txtcf.Text = par.IfNull(myReader("RAPP_CF"), "")
                    Me.txtrecapito.Text = par.IfNull(myReader("RAPP_RECAPITO"), "")
                End If
                myReader.Close()

                BindGrid()

            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabDettagglio - CaricaDati -" & ex.Message
        End Try

    End Sub
    Private Sub BindGrid()
        '*******************RICHIAMO LA CONNESSIONE*********************
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        '*******************RICHIAMO LA TRANSAZIONE*********************
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans


        par.cmd.CommandText = "SELECT DISTINCT ID_ANAGRAFICA,(CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END) AS NOMINATIVO," _
                            & "(CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN PARTITA_IVA  ELSE COD_FISCALE END) AS CF_PIVA," _
                            & "(INDIRIZZO_RESIDENZA ||'  '|| CIVICO_RESIDENZA||', '|| CAP_RESIDENZA||' '||COMUNE_RESIDENZA||'('||PROVINCIA_RESIDENZA||')')AS INDIRIZZO " _
                            & "FROM SISCOM_MI.AUTOGESTIONI_COMITATO, SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.ID = AUTOGESTIONI_COMITATO.ID_ANAGRAFICA AND AUTOGESTIONI_COMITATO.ID_ESERCIZIO = " & CType(Me.Page, Object).vIdEsercizio
        par.cmd.CommandText = par.cmd.CommandText & " ORDER BY NOMINATIVO ASC "

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt As New Data.DataTable()

        da.Fill(dt)
        CType(Me.Page, Object).vNumComitato = dt.Rows.Count
        DataGridComitato.DataSource = dt
        DataGridComitato.DataBind()

    End Sub

    Protected Sub DataGridComitato_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridComitato.ItemDataBound
        'If e.Item.ItemType = ListItemType.Item Then
        '    '---------------------------------------------------         
        '    ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
        '    '---------------------------------------------------         
        '    e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
        '    e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
        '    e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la l\'Autogestione: " & e.Item.Cells(2).Text.Replace("'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtidEsercizio').value='" & e.Item.Cells(1).Text & "'")

        'End If
        'If e.Item.ItemType = ListItemType.AlternatingItem Then
        '    '---------------------------------------------------         
        '    ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
        '    '---------------------------------------------------         
        '    e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
        '    e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
        '    e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la l\'Autogestione: " & e.Item.Cells(2).Text.Replace("'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtidEsercizio').value='" & e.Item.Cells(1).Text & "'")
        'End If

    End Sub
End Class
