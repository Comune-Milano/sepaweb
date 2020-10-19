
Partial Class Contratti_Anagrafica_RisultatiRicerca
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If


        If Not IsPostBack Then
            Nome = UCase(Request.QueryString("NOME"))
            Cognome = UCase(Request.QueryString("COGNOME"))
            CF = UCase(Request.QueryString("CF"))
            RagSociale = UCase(Request.QueryString("RAGSOCIALE"))
            PIVA = UCase(Request.QueryString("PIVA"))
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
    Private Property RagSociale() As String
        Get
            If Not (ViewState("par_RagSociale") Is Nothing) Then
                Return CStr(ViewState("par_RagSociale"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_RagSociale") = value
        End Set

    End Property
    Private Property PIVA() As String
        Get
            If Not (ViewState("par_PIVA") Is Nothing) Then
                Return CStr(ViewState("par_PIVA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_PIVA") = value
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
            da.Fill(ds, "ANAGRAFICA,COMP_NUCLEO")
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & e.Item.Cells(0).Text & "';document.getElementById('CFISCALE').value='" & e.Item.Cells(2).Text & "';document.getElementById('IDC').value='" & e.Item.Cells(1).Text & "';")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & e.Item.Cells(0).Text & "';document.getElementById('CFISCALE').value='" & e.Item.Cells(2).Text & "';document.getElementById('IDC').value='" & e.Item.Cells(1).Text & "';")

        End If

    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If

    End Sub



    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If Me.CFISCALE.Value <> "" Then
            'Response.Redirect("Inserimento.aspx?C=RisultatiRicerca&ID=" & txtid.Text & "&NOME=" & Nome & "&COGNOME=" & Cognome & "&RAGSOCIALE=" & RagSociale & "&CF=" & CF & "&PIVA=" & PIVA)

            ' Session.Add("CFRinnovoBox", CF)
            ' Response.Write("<script>self.close();</script>")

        Else
            Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
        End If

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
                sStringaSql = sStringaSql & "ANAGRAFICA.NOME" & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
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
                sStringaSql = sStringaSql & "ANAGRAFICA.COGNOME" & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
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
                sStringaSql = sStringaSql & "ANAGRAFICA.cod_fiscale " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
                bTrovato = True
            End If

            If RagSociale <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sValore = RagSociale
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                sStringaSql = sStringaSql & "ANAGRAFICA.RAGIONE_SOCIALE" & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
                bTrovato = True

            End If
            If PIVA <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sValore = PIVA
                sCompara = " = "
                sStringaSql = sStringaSql & "ANAGRAFICA.PARTITA_IVA" & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
                bTrovato = True

            End If

            'MAX 21/07/2015 DEVE CERCARE SU TUTTI I COMPONENTI
            'If bTrovato = True Then
            '    sStringaSql = "SELECT anagrafica.ID, CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE replace(RTRIM(LTRIM(COGNOME ||' ' ||NOME)),'''','') END AS ""INTESTATARIO""  ,CASE WHEN anagrafica.partita_iva is not null then partita_iva else COD_FISCALE end AS ""FISCALE_PIVA"",id_contratto,rapporti_utenza.cod_contratto,rapporti_utenza.tipo_cor||' '||rapporti_utenza.via_cor||' '||rapporti_utenza.civico_cor as indirizzo  FROM SISCOM_MI.ANAGRAFICA,siscom_mi.soggetti_contrattuali,siscom_mi.rapporti_utenza WHERE rapporti_utenza.cod_contratto is not null and siscom_mi.getstatocontratto(rapporti_utenza.id)<>'CHIUSO' AND rapporti_utenza.id=soggetti_contrattuali.id_contratto and soggetti_contrattuali.id_anagrafica=anagrafica.id and soggetti_contrattuali.cod_tipologia_occupante='INTE' and " & sStringaSql & " order by INTESTATARIO asc"
            'Else
            '    sStringaSql = "SELECT anagrafica.ID, CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE replace(RTRIM(LTRIM(COGNOME ||' ' ||NOME)),'''','') END AS ""INTESTATARIO""  ,CASE WHEN anagrafica.partita_iva is not null then partita_iva else COD_FISCALE end AS ""FISCALE_PIVA"",id_contratto,rapporti_utenza.cod_contratto,rapporti_utenza.tipo_cor||' '||rapporti_utenza.via_cor||' '||rapporti_utenza.civico_cor as indirizzo  FROM SISCOM_MI.ANAGRAFICA,siscom_mi.soggetti_contrattuali,siscom_mi.rapporti_utenza WHERE rapporti_utenza.cod_contratto is not null and siscom_mi.getstatocontratto(rapporti_utenza.id)<>'CHIUSO' AND rapporti_utenza.id=soggetti_contrattuali.id_contratto and soggetti_contrattuali.id_anagrafica=anagrafica.id and soggetti_contrattuali.cod_tipologia_occupante='INTE' order by INTESTATARIO asc"
            'End If
            If bTrovato = True Then
                sStringaSql = "SELECT anagrafica.ID, CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE replace(RTRIM(LTRIM(COGNOME ||' ' ||NOME)),'''','') END AS ""INTESTATARIO""  ,CASE WHEN anagrafica.partita_iva is not null then partita_iva else COD_FISCALE end AS ""FISCALE_PIVA"",'' AS id_contratto,'' AS cod_contratto,'' AS indirizzo  FROM SISCOM_MI.ANAGRAFICA WHERE " & sStringaSql & " order by INTESTATARIO asc"
            Else
                sStringaSql = "SELECT anagrafica.ID, CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE replace(RTRIM(LTRIM(COGNOME ||' ' ||NOME)),'''','') END AS ""INTESTATARIO""  ,CASE WHEN anagrafica.partita_iva is not null then partita_iva else COD_FISCALE end AS ""FISCALE_PIVA"",'' AS id_contratto,'' AS cod_contratto,'' AS indirizzo  FROM SISCOM_MI.ANAGRAFICA order by INTESTATARIO asc"
            End If

            BindGrid()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            'par.OracleConn.Close()
        End Try

    End Sub


    Protected Sub DataGrid1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged

    End Sub
End Class
