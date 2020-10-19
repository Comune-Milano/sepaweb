
Partial Class Contratti_EliminaBozze
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()

        If Not IsPostBack Then
            btnVisualizza.Attributes.Add("onclick", "this.style.visibility='hidden'")
            LBLID.Value = "-1"
            Cerca()
        End If

    End Sub

    Private Sub BindGrid()
        Try



            par.OracleConn.Open()

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

            Dim ds As New Data.DataSet()

            da.Fill(ds, "SISCOM_MI.RAPPORTI_UTENZA")
            Label4.Text = Datagrid2.Items.Count
            Datagrid2.DataSource = ds
            Datagrid2.DataBind()
            Label4.Text = "  - " & Datagrid2.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            TextBox3.Text = ex.Message

        End Try
    End Sub

    Private Function Cerca()
        Dim bTrovato As Boolean



        bTrovato = False


        sStringaSQL1 = "SELECT UNITA_IMMOBILIARI.ID AS IDUNITA,UNITA_IMMOBILIARI.COD_TIPOLOGIA,INDIRIZZI.DESCRIZIONE AS ""INDIRIZZO"",INDIRIZZI.CIVICO,UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE,nvl(rapporti_utenza.n_offerta,'0') N_OFFERTA,RAPPORTI_UTENZA.*,CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS ""INTESTATARIO"" ,CASE WHEN anagrafica.partita_iva is not null then partita_iva else COD_FISCALE end AS ""COD FISCALE/PIVA"" ," _
                     & "TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE AS ""POSIZIONE_CONTRATTO"" FROM SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA," _
                     & "SISCOM_MI.INDIRIZZI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE TIPOLOGIA_RAPP_CONTRATTUALE.COD=RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR  " _
                     & " AND EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND INDIRIZZI.ID=EDIFICI.ID_INDIRIZZO_PRINCIPALE AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND " _
                     & "ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID)='BOZZA'"


        sStringaSQL1 = sStringaSQL1 & " ORDER BY ""INTESTATARIO"" ASC"
        BindGrid()

    End Function

    Public Property sStringaSQL1() As String
        Get
            If Not (ViewState("par_sStringaSQL1") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL1") = value
        End Set

    End Property

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")

    End Sub

    Protected Sub Datagrid2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Datagrid2.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TextBox3').value='Hai selezionato il contratto Cod. " & e.Item.Cells(1).Text & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';document.getElementById('Label3').value='" & e.Item.Cells(1).Text & "';document.getElementById('HiddenField2').value='" & e.Item.Cells(2).Text & "';document.getElementById('IDUNITA').value='" & e.Item.Cells(3).Text & "';")

            'btnVisualizza.Attributes.Add("onclick", "window.open('Contratto.aspx?ID=" & LBLID.Text & "&COD=" & Label3.Text & "','Contratto" & Format(Now, "hhss") & "','height=680,width=900');")
        End If
    End Sub

    Protected Sub Datagrid2_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid2.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            Datagrid2.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub


    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click

        Try

            Dim INDIRIZZOUNITA As String = ""
            Dim INTESTATARIO As String = ""
            Dim BOLLETTE As String = ""

            If HiddenField1.Value = "1" Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans



                If HiddenField2.Value <> "&nbsp;" Then

                    par.cmd.CommandText = "select INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO,INDIRIZZI.CAP,INDIRIZZI.LOCALITA FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI WHERE INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID=" & IDUNITA.Value
                    Dim myReaderAX1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderAX1.Read Then
                        INDIRIZZOUNITA = par.IfNull(myReaderAX1("DESCRIZIONE"), "") & ", " & par.IfNull(myReaderAX1("CIVICO"), "") & " CAP " & par.IfNull(myReaderAX1("CAP"), "") & " " & par.IfNull(myReaderAX1("LOCALITA"), "")
                    End If
                    myReaderAX1.Close()

                    par.cmd.CommandText = "SELECT COGNOME,NOME,RAGIONE_SOCIALE FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE SOGGETTI_CONTRATTUALI.ID_CONTRATTO=" & LBLID.Value & " AND COD_TIPOLOGIA_OCCUPANTE='INTE' AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA"
                    myReaderAX1 = par.cmd.ExecuteReader()
                    Do While myReaderAX1.Read
                        INTESTATARIO = INTESTATARIO & par.IfNull(myReaderAX1("COGNOME"), "") & " " & par.IfNull(myReaderAX1("NOME"), "") & " " & par.IfNull(myReaderAX1("RAGIONE_SOCIALE"), "") & " "
                    Loop
                    myReaderAX1.Close()

                    BOLLETTE = ""

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE WHERE ID_CONTRATTO=" & LBLID.Value & " AND ID_BOLLETTA_STORNO IS NULL AND ID_TIPO <>22 " _
                        & " AND ID NOT IN (SELECT BBV.ID_BOLLETTA FROM SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI BBVP, SISCOM_MI.BOL_BOLLETTE_VOCI BBV WHERE BBVP.ID_VOCE_BOLLETTA = BBV.ID AND BBV.ID_BOLLETTA = BOL_BOLLETTE.ID) "
                    myReaderAX1 = par.cmd.ExecuteReader()
                    Do While myReaderAX1.Read
                        BOLLETTE = BOLLETTE & Format(par.IfNull(myReaderAX1("ID"), ""), "0000000000") & " Emessa il " & par.FormattaData(par.IfNull(myReaderAX1("data_emissione"), "")) & " - "
                    Loop
                    myReaderAX1.Close()

                    par.cmd.CommandText = "select id from siscom_mi.segnalazioni where id_stato<>2 and id_contratto=" & LBLID.Value
                    Dim idSegn As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
                    If idSegn <> 0 Then
                        par.myTrans.Rollback()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Response.Write("<script>alert('Non è possibile eliminare il Contratto perchè risultano esserci segnalazioni aperte. Contattare l\'Amministratore del sistema per eventuali chiarimenti.');</script>")
                        LBLID.Value = ""
                        TextBox3.Text = "Nessuna Selezione"
                        BindGrid()
                        Exit Sub
                    End If


                    If BOLLETTE = "" Then

                        'ARPA 04/07/2018
                        par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.ARPA_ELABORAZIONI_UNITA WHERE ID_CONTRATTO = " & LBLID.Value
                        Dim ContrattoEsiste As Long = par.IfEmpty(par.IfNull(par.cmd.ExecuteScalar, 0), 0)
                        If ContrattoEsiste > 0 Then
                            par.cmd.CommandText = "DELETE FROM SISCOM_MI.ARPA_ELABORAZIONI_INQUILINI WHERE ID_CONTRATTO = " & LBLID.Value
                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = "DELETE FROM SISCOM_MI.ARPA_ELABORAZIONI_NUCLEI WHERE ID_CONTRATTO = " & LBLID.Value
                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = "UPDATE SISCOM_MI.ARPA_ELABORAZIONI_UNITA SET ID_CONTRATTO = NULL WHERE ID_CONTRATTO = " & LBLID.Value
                            par.cmd.ExecuteNonQuery()
                        End If
                        'ARPA 04/07/2018


                        'AUTORIZZAZIONE A CANCELLAZIONE DI BOLLETTE STORNATE E NON PAGATE
                        par.cmd.CommandText = "delete from siscom_mi.bol_bollette_voci_emissioni where id_bol_bollette_voci in (select id from siscom_mi.bol_bollette_voci where id_bolletta in (select id from siscom_mi.bol_bollette where id_contratto = " & LBLID.Value & "))"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "update siscom_mi.bol_bollette set fl_stampato = 0 ,rif_bollettino = null where id_contratto = " & LBLID.Value
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "delete from siscom_mi.rapporti_utenza_dep_prov where id_contratto = " & LBLID.Value
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "delete from siscom_mi.segnalazioni where id_stato=2 and id_contratto=" & LBLID.Value
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "delete from siscom_mi.storico_dep_cauzionale where id_contratto = " & LBLID.Value
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_ELIMINATI (COD_CONTRATTO,INDIRIZZO,DATA_ELIMINAZIONE,ID_OPERATORE,BOLLETTE,INTESTATARIO) " _
                                  & " VALUES ('" & Label3.Value & "','" & par.PulisciStrSql(INDIRIZZOUNITA) _
                                  & "','" & Format(Now, "yyyyMMdd") & "'," & Session.Item("ID_OPERATORE") _
                                  & ",'" & par.PulisciStrSql(BOLLETTE) & "','" & par.PulisciStrSql(INTESTATARIO) & "')"

                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "delete from SISCOM_MI.RAPPORTI_UTENZA_DEP_PROV where id_contratto=" & LBLID.Value
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID=" & LBLID.Value
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "update SISCOM_MI.UNITA_ASSEGNATE SET GENERATO_CONTRATTO=0 WHERE N_OFFERTA=" & HiddenField2.Value & " AND ID_UNITA=" & IDUNITA.Value
                        par.cmd.ExecuteNonQuery()

                        par.myTrans.Commit()
                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Response.Write("<script>alert('Contratto eliminato');</script>")
                        LBLID.Value = ""
                        TextBox3.Text = "Nessuna Selezione"
                        BindGrid()
                    Else
                        par.myTrans.Rollback()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Response.Write("<script>alert('Non è possibile eliminare il Contratto perchè sono state emesse bollette. Contattare l\'Amministratore del sistema per eventuali chiarimenti.');</script>")
                        LBLID.Value = ""
                        TextBox3.Text = "Nessuna Selezione"
                        BindGrid()
                    End If

                Else
                    par.myTrans.Rollback()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Response.Write("<script>alert('Non è possibile eliminare il Contratto. Contattare l\'Amministratore del sistema.');</script>")
                    LBLID.Value = ""
                    TextBox3.Text = "Nessuna Selezione"
                    BindGrid()
                End If
            End If
        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                par.myTrans.Rollback()
                par.OracleConn.Close()
                Dim scriptblock As String = ""
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Contratto aperto da un altro utente. Non è possibile eliminare al momento!');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                End If
            Else
                par.myTrans.Rollback()
                par.OracleConn.Close()
                TextBox3.Text = EX1.Message
            End If

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            TextBox3.Text = ex.Message
        End Try
    End Sub



End Class
