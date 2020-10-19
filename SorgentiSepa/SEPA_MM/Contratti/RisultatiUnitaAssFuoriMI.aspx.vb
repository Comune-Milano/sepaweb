
Partial Class Contratti_RisultatiUnitaAssFuoriMI
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            Nome = Request.QueryString("NOME")
            Cognome = Request.QueryString("COGNOME")
            CF = Request.QueryString("CF")
            Cerca()
        End If
        
    End Sub

    Private Property Nome() As String
        Get
            If Not (ViewState("par_Nome") Is Nothing) Then
                Return CStr(ViewState("par_Nome"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Nome") = value
        End Set

    End Property
    Private Property Cognome() As String
        Get
            If Not (ViewState("par_Cognome") Is Nothing) Then
                Return CStr(ViewState("par_Cognome"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Cognome") = value
        End Set

    End Property
    Private Property CF() As String
        Get
            If Not (ViewState("par_CF") Is Nothing) Then
                Return CStr(ViewState("par_CF"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_CF") = value
        End Set

    End Property

    Private Property sStringaSql() As String
        Get
            If Not (ViewState("par_sStringaSql") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSql"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSql") = value
        End Set

    End Property

    Private Sub Cerca()
        Try
            Dim bTrovato As Boolean = False
            Dim condizione As String = ""
            Dim sValore As String = ""
            Dim sCompara As String

            par.OracleConn.Open()
            par.SettaCommand(par)

            If Nome <> "" Then
                sValore = Nome
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                sStringaSql = sStringaSql & "SISCOM_MI.UNITA_ASSEGNATE.NOME " & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
                bTrovato = True
            End If

            If Cognome <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sValore = Cognome
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)

                Else
                    sCompara = " = "
                End If
                sStringaSql = sStringaSql & " SISCOM_MI.UNITA_ASSEGNATE.COGNOME_RS" & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
                bTrovato = True

            End If


            If CF <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sValore = CF
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)

                Else
                    sCompara = " = "
                End If
                sStringaSql = sStringaSql & "SISCOM_MI.UNITA_ASSEGNATE.CF_PIVA" & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
                bTrovato = True
            End If

            Dim TipoRicerca As String = ""
            TipoRicerca = "AND SISCOM_MI.UNITA_ASSEGNATE.PROVENIENZA = 'E' AND PROVVEDIMENTO IS NOT NULL " 'ERP


            If bTrovato = True Then
                sStringaSql = "SELECT ID_DOMANDA,ID_UNITA, UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE  ,TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA,RTRIM(LTRIM(COGNOME_RS ||' ' ||NOME)) AS ""INTESTATARIO"" ,CF_PIVA,  TO_CHAR(TO_DATE(data_assegnazione,'yyyymmdd'),'dd/mm/yyyy') AS DATA_ASSEGNAZIONE ,RTRIM(LTRIM(SISCOM_MI.INDIRIZZI.DESCRIZIONE ||','||SISCOM_MI.INDIRIZZI.CIVICO))AS ""INDIRIZZO"",UNITA_ASSEGNATE.N_OFFERTA,UNITA_ASSEGNATE.ID_DICHIARAZIONE,unita_assegnate.provenienza FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.UNITA_ASSEGNATE, SISCOM_MI.INDIRIZZI, SISCOM_MI.EDIFICI, SISCOM_MI.UNITA_IMMOBILIARI WHERE " & sStringaSql & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD=UNITA_IMMOBILIARI.COD_TIPOLOGIA AND GENERATO_CONTRATTO = 0 AND SISCOM_MI.UNITA_IMMOBILIARI.ID =SISCOM_MI.UNITA_ASSEGNATE.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO= EDIFICI.ID AND SISCOM_MI.INDIRIZZI.ID = SISCOM_MI.EDIFICI.ID_INDIRIZZO_PRINCIPALE(+) " & TipoRicerca & " and UNITA_ASSEGNATE.id_dichiarazione in (select id_dichiarazione from DICH_ASSEGN_FUORI_MI where DICH_ASSEGN_FUORI_MI.id_dichiarazione=UNITA_ASSEGNATE.id_dichiarazione) ORDER BY DATA_ASSEGNAZIONE DESC"
            Else
                sStringaSql = "SELECT ID_DOMANDA,ID_UNITA, UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE  ,TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA,RTRIM(LTRIM(COGNOME_RS ||' ' ||NOME)) AS ""INTESTATARIO"" ,CF_PIVA,  TO_CHAR(TO_DATE(data_assegnazione,'yyyymmdd'),'dd/mm/yyyy') AS DATA_ASSEGNAZIONE ,RTRIM(LTRIM(SISCOM_MI.INDIRIZZI.DESCRIZIONE ||','||SISCOM_MI.INDIRIZZI.CIVICO))AS ""INDIRIZZO"",UNITA_ASSEGNATE.N_OFFERTA,UNITA_ASSEGNATE.ID_DICHIARAZIONE,unita_assegnate.provenienza FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.UNITA_ASSEGNATE, SISCOM_MI.INDIRIZZI, SISCOM_MI.EDIFICI, SISCOM_MI.UNITA_IMMOBILIARI WHERE TIPOLOGIA_UNITA_IMMOBILIARI.COD=UNITA_IMMOBILIARI.COD_TIPOLOGIA AND GENERATO_CONTRATTO = 0 AND SISCOM_MI.UNITA_IMMOBILIARI.ID =SISCOM_MI.UNITA_ASSEGNATE.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO= EDIFICI.ID AND SISCOM_MI.INDIRIZZI.ID = SISCOM_MI.EDIFICI.ID_INDIRIZZO_PRINCIPALE(+) " & TipoRicerca & " and UNITA_ASSEGNATE.id_dichiarazione in (select id_dichiarazione from DICH_ASSEGN_FUORI_MI where DICH_ASSEGN_FUORI_MI.id_dichiarazione=UNITA_ASSEGNATE.id_dichiarazione) ORDER BY DATA_ASSEGNAZIONE DESC"
            End If


            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSql, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            dgvElencoPratiche.DataSource = dt
            dgvElencoPratiche.DataBind()

            lblNumTot.Text = dt.Rows.Count

            par.OracleConn.Close()
        Catch ex As Exception
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - Cerca - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub dgvElencoPratiche_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvElencoPratiche.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='yellow';};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='red';" _
                                & "document.getElementById('txtmia').value='Hai selezionato: " & Replace(e.Item.Cells(par.IndDGC(dgvElencoPratiche, "INTESTATARIO")).Text, "'", "\'") & "';" _
                                & "document.getElementById('idUnita').value='" & e.Item.Cells(par.IndDGC(dgvElencoPratiche, "ID_UNITA")).Text & "';document.getElementById('idDich').value='" & e.Item.Cells(par.IndDGC(dgvElencoPratiche, "ID_DICHIARAZIONE")).Text & "';")
            e.Item.Attributes.Add("onDblclick", "document.getElementById('btnProcedi').click();")
        End If
    End Sub

    Protected Sub dgvElencoPratiche_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvElencoPratiche.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            dgvElencoPratiche.CurrentPageIndex = e.NewPageIndex
            Cerca()
        End If
    End Sub

    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        If Me.txtmia.Text <> "" Then
            If idDich.Value <> "" And idUnita.Value <> "" Then
                Try
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    par.cmd.CommandText = "SELECT GENERATO_CONTRATTO FROM SISCOM_MI.UNITA_ASSEGNATE WHERE ID_DICHIARAZIONE=" & idDich.Value & " AND ID_UNITA=" & idUnita.Value & " ORDER BY DATA_ASSEGNAZIONE DESC"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        If myReader("GENERATO_CONTRATTO") = "1" Then
                            Response.Write("<script>alert('Il contratto è stato già creato. Aggiornare la lista!');</script>")
                        Else
                            Response.Write("<script>A=window.open('Contratto.aspx?ID=-1&IdDichiarazione=" & idDich.Value & "&IdDomanda=-1&IdUnita=" & idUnita.Value & "&TIPO=1&Lett=FM','Contratto" & Format(Now, "hhss") & "','height=780,width=1160');A.focus();</script>")
                        End If
                    End If
                    myReader.Close()
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Catch ex As Exception
                    par.OracleConn.Close()
                    Session.Add("ERRORE", "Provenienza:" & Page.Title & " - Procedi - " & ex.Message)
                    Response.Redirect("../Errore.aspx", False)
                End Try
            End If
        End If

    End Sub
End Class
