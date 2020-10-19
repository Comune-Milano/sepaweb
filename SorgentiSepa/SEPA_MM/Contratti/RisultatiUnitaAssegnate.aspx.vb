
Partial Class Contratti_RisultatiUnitaAssegnate
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Try
            If Not IsPostBack Then
                Nome = Request.QueryString("NOME")
                Cognome = Request.QueryString("COGNOME")
                CF = Request.QueryString("CF")
                Offerta = Request.QueryString("OFF")
                Cerca()
            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

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

    Private Property Offerta() As String
        Get
            If Not (ViewState("par_Offerta") Is Nothing) Then
                Return CStr(ViewState("par_Offerta"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Offerta") = value
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

    Private Sub BindGrid()
        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
            End If

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSql, par.OracleConn)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "UNITA_ASSEGNATE,COMP_NUCLEO")
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()
            Label1.Text = "Risultati Ricerca  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub

    Private Sub Cerca()
        Try

            Dim bTrovato As Boolean = False
            Dim condizione As String = ""
            Dim sValore As String = ""
            Dim sCompara As String

            'par.OracleConn.Open()
            'par.SettaCommand(par)

            'par.cmd.CommandText = "SELECT * FROM ANAGRAFICA"

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

            If offerta <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sValore = offerta
                
                sStringaSql = sStringaSql & " SISCOM_MI.UNITA_ASSEGNATE.N_OFFERTA=" & Val(par.PulisciStrSql(sValore)) & " "
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

            Select Case Request.QueryString("TIPO")
                Case "1"
                    If Request.QueryString("ORIG") = "1" Then
                        TipoRicerca = "AND SISCOM_MI.UNITA_ASSEGNATE.PROVENIENZA = 'Y'" 'cambi in emergenza
                    Else
                        TipoRicerca = "AND SISCOM_MI.UNITA_ASSEGNATE.PROVENIENZA = 'E' AND PROVVEDIMENTO IS NOT NULL " 'ERP
                    End If
                Case "2"
                    TipoRicerca = "AND SISCOM_MI.UNITA_ASSEGNATE.PROVENIENZA = 'C' AND PROVVEDIMENTO IS NOT NULL " 'CAMBI
                Case "3"
                    TipoRicerca = "AND (SISCOM_MI.UNITA_ASSEGNATE.PROVENIENZA = 'U' OR SISCOM_MI.UNITA_ASSEGNATE.PROVENIENZA = 'H') AND PROVVEDIMENTO IS NOT NULL" 'USI DIVERSI
                Case "4"
                    TipoRicerca = "AND SISCOM_MI.UNITA_ASSEGNATE.PROVENIENZA = 'P' AND PROVVEDIMENTO IS NOT NULL" 'mobilita
                Case "5"
                    TipoRicerca = "AND SISCOM_MI.UNITA_ASSEGNATE.PROVENIENZA = 'Q' AND PROVVEDIMENTO IS NOT NULL" '392/78
                Case "6"
                    TipoRicerca = "AND (SISCOM_MI.UNITA_ASSEGNATE.PROVENIENZA = 'W' or SISCOM_MI.UNITA_ASSEGNATE.PROVENIENZA = 'D') AND PROVVEDIMENTO IS NOT NULL" '431/78
                Case "7"
                    TipoRicerca = "AND SISCOM_MI.UNITA_ASSEGNATE.PROVENIENZA = 'X'" 'Abusivi
                Case "10"
                    TipoRicerca = "AND SISCOM_MI.UNITA_ASSEGNATE.PROVENIENZA = 'Z'" 'Forze dell'Ordine
                Case "11"
                    TipoRicerca = "AND SISCOM_MI.UNITA_ASSEGNATE.PROVENIENZA = 'A'" 'canone convenzionato
            End Select
            'If Request.QueryString("TIPO") <> 3 Then
            '    TipoRicerca = "AND SISCOM_MI.UNITA_ASSEGNATE.PROVENIENZA <> 'U'"
            'Else
            '    TipoRicerca = "AND SISCOM_MI.UNITA_ASSEGNATE.PROVENIENZA = 'U'"
            'End If

            If bTrovato = True Then
                sStringaSql = "SELECT ID_DOMANDA,ID_UNITA, UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE  ,TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA,RTRIM(LTRIM(COGNOME_RS ||' ' ||NOME)) AS ""INTESTATARIO"" ,CF_PIVA,  TO_CHAR(TO_DATE(data_assegnazione,'yyyymmdd'),'dd/mm/yyyy') AS DATA_ASSEGNAZIONE ,RTRIM(LTRIM(SISCOM_MI.INDIRIZZI.DESCRIZIONE ||','||SISCOM_MI.INDIRIZZI.CIVICO))AS ""INDIRIZZO"",UNITA_ASSEGNATE.N_OFFERTA,UNITA_ASSEGNATE.ID_DICHIARAZIONE,unita_assegnate.provenienza FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.UNITA_ASSEGNATE, SISCOM_MI.INDIRIZZI, SISCOM_MI.EDIFICI, SISCOM_MI.UNITA_IMMOBILIARI WHERE " & sStringaSql & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD=UNITA_IMMOBILIARI.COD_TIPOLOGIA AND GENERATO_CONTRATTO = 0 AND SISCOM_MI.UNITA_IMMOBILIARI.ID =SISCOM_MI.UNITA_ASSEGNATE.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO= EDIFICI.ID AND SISCOM_MI.INDIRIZZI.ID = SISCOM_MI.EDIFICI.ID_INDIRIZZO_PRINCIPALE(+) " & TipoRicerca & " and UNITA_ASSEGNATE.id_dichiarazione not in (select id_dichiarazione from DICH_ASSEGN_FUORI_MI where DICH_ASSEGN_FUORI_MI.id_dichiarazione=UNITA_ASSEGNATE.id_dichiarazione) ORDER BY DATA_ASSEGNAZIONE DESC"
            Else
                sStringaSql = "SELECT ID_DOMANDA,ID_UNITA, UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE  ,TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA,RTRIM(LTRIM(COGNOME_RS ||' ' ||NOME)) AS ""INTESTATARIO"" ,CF_PIVA,  TO_CHAR(TO_DATE(data_assegnazione,'yyyymmdd'),'dd/mm/yyyy') AS DATA_ASSEGNAZIONE ,RTRIM(LTRIM(SISCOM_MI.INDIRIZZI.DESCRIZIONE ||','||SISCOM_MI.INDIRIZZI.CIVICO))AS ""INDIRIZZO"",UNITA_ASSEGNATE.N_OFFERTA,UNITA_ASSEGNATE.ID_DICHIARAZIONE,unita_assegnate.provenienza FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.UNITA_ASSEGNATE, SISCOM_MI.INDIRIZZI, SISCOM_MI.EDIFICI, SISCOM_MI.UNITA_IMMOBILIARI WHERE TIPOLOGIA_UNITA_IMMOBILIARI.COD=UNITA_IMMOBILIARI.COD_TIPOLOGIA AND GENERATO_CONTRATTO = 0 AND SISCOM_MI.UNITA_IMMOBILIARI.ID =SISCOM_MI.UNITA_ASSEGNATE.ID_UNITA AND SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO= EDIFICI.ID AND SISCOM_MI.INDIRIZZI.ID = SISCOM_MI.EDIFICI.ID_INDIRIZZO_PRINCIPALE(+) " & TipoRicerca & " and UNITA_ASSEGNATE.id_dichiarazione not in (select id_dichiarazione from DICH_ASSEGN_FUORI_MI where DICH_ASSEGN_FUORI_MI.id_dichiarazione=UNITA_ASSEGNATE.id_dichiarazione) ORDER BY DATA_ASSEGNAZIONE DESC"
            End If


            BindGrid()
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try
    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & Replace(e.Item.Cells(0).Text, "'", "\'") & "';document.getElementById('txtiddomanda').value='" & e.Item.Cells(1).Text & "';document.getElementById('txtidunita').value='" & e.Item.Cells(2).Text & "';document.getElementById('txtIdDichiarazione').value='" & e.Item.Cells(3).Text & "';document.getElementById('txtprovenienza').value='" & e.Item.Cells(4).Text & "';")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & Replace(e.Item.Cells(0).Text, "'", "\'") & "';document.getElementById('txtiddomanda').value='" & e.Item.Cells(1).Text & "';document.getElementById('txtidunita').value='" & e.Item.Cells(2).Text & "';document.getElementById('txtIdDichiarazione').value='" & e.Item.Cells(3).Text & "';document.getElementById('txtprovenienza').value='" & e.Item.Cells(4).Text & "';")

            



        End If

    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If

    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If Me.txtmia.Text <> "" Then

            Dim IdUnita As String
            Dim idDomanda As String
            Dim IDDICHIARAZIONE As String

            IdUnita = txtidunita.Value
            idDomanda = txtiddomanda.Value
            IDDICHIARAZIONE = txtIdDichiarazione.Value

            Dim Tipo
            Select Case Request.QueryString("tipo")
                Case "1"
                    Tipo = "1"
                Case "2"
                    Tipo = "2"
                Case "5"
                    Tipo = "5"
                Case "6"
                    Tipo = "6"
                Case "7"
                    Tipo = "7"
                Case "10"
                    Tipo = "10"
                Case "4"
                    Tipo = "11"
                Case "11"
                    Tipo = "12"
                Case Else
                    Tipo = "3"
            End Select

            If idDomanda <> "" And IdUnita <> "" Then
                Try
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    par.cmd.CommandText = "SELECT GENERATO_CONTRATTO FROM SISCOM_MI.UNITA_ASSEGNATE WHERE ID_DOMANDA=" & idDomanda & " AND ID_UNITA=" & IdUnita & " ORDER BY DATA_ASSEGNAZIONE DESC"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        If myReader("GENERATO_CONTRATTO") = "1" Then
                            Response.Write("<script>alert('Il contratto è stato già creato. Aggiornare la lista!');</script>")
                        Else
                            Response.Write("<script>A=window.open('Contratto.aspx?ID=-1&IdDichiarazione=" & IDDICHIARAZIONE & "&IdDomanda=" & idDomanda & "&IdUnita=" & IdUnita & "&TIPO=" & Tipo & "&Orig=" & Request.QueryString("ORIG") & "&Lett=" & txtprovenienza.Value & "','Contratto" & Format(Now, "hhss") & "','height=780,width=1160');A.focus();</script>")
                        End If
                    End If
                    myReader.Close()
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Catch ex As Exception
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Response.Write(ex.Message)
                End Try
            End If
        End If

    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>parent.main.location.replace('NuovoContratto.aspx?TIPO=" & Request.QueryString("TIPO") & "&ORIG=" & Request.QueryString("ORIG") & "')</script>")
    End Sub



End Class
