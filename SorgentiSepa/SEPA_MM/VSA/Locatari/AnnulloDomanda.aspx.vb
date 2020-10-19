
Partial Class VSA_Locatari_AnnulloDomanda
    Inherits System.Web.UI.Page
    Public connData As CM.datiConnessione = Nothing
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)

        txtDataPr.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtDataEvento.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtDataIn.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtDataFine.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
            & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
            & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
            & "margin-top: -48px; background-image: url('../../NuoveImm/sfondo.png');"">" _
            & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
            & "<img src=""../../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
            & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
            & "</td></tr></table></div></div>"

        Response.Write(Loading)
        Response.Flush()
    End Sub


    Protected Sub btnCercaPG_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCercaPG.Click
        Try
            connData.apri(False)
            Dim dettagliRU As String = ""
            Dim idRu As Long = 0
            If txtPG.Text <> "" Then
                par.cmd.CommandText = "SELECT getdata(domande_bando_vsa.data_pg) as data_inserimento,(select descrizione from t_motivo_domanda_vsa where id=id_motivo_domanda) as tipo_domanda,data_evento,data_presentazione,contratto_num, " _
                    & "data_inizio_val,data_fine_val FROM domande_bando_vsa,dichiarazioni_vsa WHERE domande_bando_vsa.PG='" & par.PulisciStrSql(txtPG.Text) & "' and domande_bando_vsa.id_dichiarazione=dichiarazioni_vsa.id"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    lblErr.Visible = False
                    btnProcedi.Visible = True
                    codcontratto.Value = par.IfNull(myReader("contratto_num"), "")
                    txtDataEvento.Text = par.FormattaData(par.IfNull(myReader("data_evento"), ""))
                    txtDataPr.Text = par.FormattaData(par.IfNull(myReader("data_presentazione"), ""))
                    txtDataIn.Text = par.FormattaData(par.IfNull(myReader("data_inizio_val"), ""))
                    txtDataFine.Text = par.FormattaData(par.IfNull(myReader("data_fine_val"), ""))

                    par.cmd.CommandText = "select id from siscom_mi.rapporti_utenza where cod_contratto='" & codcontratto.Value & "'"
                    idRu = par.IfNull(par.cmd.ExecuteScalar, 0)

                    dettagliRU = "<a href='#' onclick=" & Chr(34) & "javascript:window.open('../../Contratti/Contratto.aspx?LT=1&ID=" & idRu & "&COD=" & par.IfNull(myReader("contratto_num"), "") & "','Contratto_" & Format(Now, "mmss") & "','height=780,width=1160');" & Chr(34) & " alt='Visualizza Contratto'>" & par.IfNull(myReader("contratto_num"), "") & "</a>"

                    lblInfoDomanda.Text = "Rapporto cod. " & dettagliRU & " domanda di <b>" & myReader("tipo_domanda") & "</b> del " & myReader("data_inserimento")
                Else
                    btnProcedi.Visible = False
                    lblErr.Visible = True
                End If
                myReader.Close()
            Else
                Response.Write("<script>alert('Inserire il numero della domanda!');</script>")
            End If
            connData.chiudi(False)

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: btnCerca_Click - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)

        End Try
    End Sub

    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        Try
            connData.apri(True)

            Dim idDichiarazione As Long = 0
            Dim idDomanda As Long = 0
            Dim canone As Decimal = 0
            Dim fl_contabilizzato As Integer = 0
            Dim fl_autorizzazione As Integer = 0
            Dim idMotivoDomanda As Integer = -1

            par.cmd.CommandText = "SELECT * from domande_bando_vsa WHERE pg='" & par.PulisciStrSql(txtPG.Text) & "' for update nowait"
            Dim myreader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            myreader.Close()

            par.cmd.CommandText = "SELECT * from domande_bando_vsa WHERE pg='" & par.PulisciStrSql(txtPG.Text) & "'"
            Dim myreaderVsa As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myreaderVsa.Read Then
                idDichiarazione = par.IfNull(myreaderVsa("id_dichiarazione"), 0)
                idDomanda = par.IfNull(myreaderVsa("id"), 0)
                fl_contabilizzato = par.IfNull(myreaderVsa("fl_contabilizzato"), 0)
                fl_autorizzazione = par.IfNull(myreaderVsa("fl_autorizzazione"), 0)
                idMotivoDomanda = par.IfNull(myreaderVsa("id_motivo_domanda"), -1)

                par.cmd.CommandText = "UPDATE domande_bando_vsa SET fl_autorizzazione = 0,fl_contabilizzato = 0,data_autorizzazione=''," _
                    & "data_presentazione=" & par.insDbValue(txtDataPr.Text, True, True) & "," _
                    & "data_evento=" & par.insDbValue(txtDataEvento.Text, True, True) & "," _
                    & "credito_teorico='',ct_al='',ct_dal='' WHERE id =" & myreaderVsa("ID")
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM eventi_bandi_vsa WHERE cod_evento in ('F179','F165','F196','F197','F204','F208','F210','F212','F213') AND id_domanda=" & myreaderVsa("ID")
                par.cmd.ExecuteNonQuery()

                If idMotivoDomanda = 3 Then
                    If fl_contabilizzato = 0 Then
                        par.cmd.CommandText = "DELETE FROM vsa_crediti_rid_canone WHERE id_domanda=" & myreaderVsa("ID")
                        par.cmd.ExecuteNonQuery()
                    End If
                End If
            End If
            myreaderVsa.Close()

            par.cmd.CommandText = "select * from dichiarazioni_vsa where id=" & idDichiarazione & " for update nowait"
            Dim myreaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            myreaderD.Close()

            par.cmd.CommandText = "update dichiarazioni_vsa set data_inizio_val=" & par.insDbValue(txtDataIn.Text, True, True) & ",data_fine_val=" & par.insDbValue(txtDataFine.Text, True, True) & " where id=" & idDichiarazione
            par.cmd.ExecuteNonQuery()

            If fl_autorizzazione = 1 Then
                Select Case idMotivoDomanda
                    Case 0  'CAMBIO INTESTAZIONE
                        connData.chiudi(False)
                        Response.Write("<script>alert('Attenzione, non è possibile annullare un cambio intestazione autorizzato!');</script>")
                        Exit Sub
                    Case 1, 6  'VARIAZIONE INTESTAZIONE
                        par.cmd.CommandText = "update siscom_mi.soggetti_contrattuali set cod_tipologia_occupante='ALTR' where cod_tipologia_occupante='INTE' and id_contratto=(select id from siscom_mi.rapporti_utenza where cod_contratto='" & codcontratto.Value & "') "
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "update siscom_mi.soggetti_contrattuali set cod_tipologia_occupante='INTE',data_fine='29991231' where cod_tipologia_occupante='EXINTE' and id_contratto=(select id from siscom_mi.rapporti_utenza where cod_contratto='" & codcontratto.Value & "') "
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "delete from siscom_mi.rapporti_utenza_cessioni where id_contratto=(select id from siscom_mi.rapporti_utenza where cod_contratto='" & codcontratto.Value & "')"
                        par.cmd.ExecuteNonQuery()

                    Case 2  'AMPLIAMENTO
                        par.cmd.CommandText = "select id_anagrafica, id_contratto from siscom_mi.soggetti_contrattuali where id_contratto=(select id from siscom_mi.rapporti_utenza where cod_contratto='" & codcontratto.Value & "') and data_inizio= " _
                            & " (select max(data_inizio) from siscom_mi.soggetti_contrattuali sc where sc.id_contratto=soggetti_contrattuali.id_contratto )"
                        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                        Dim dt As New Data.DataTable
                        da.Fill(dt)
                        da.Dispose()
                        If dt.Rows.Count > 0 Then
                            For Each row As Data.DataRow In dt.Rows
                                par.cmd.CommandText = "delete from siscom_mi.soggetti_contrattuali where id_anagrafica=" & row.Item("id_anagrafica") & " and id_contratto=(select id from siscom_mi.rapporti_utenza where cod_contratto='" & codcontratto.Value & "')"
                                par.cmd.ExecuteNonQuery()
                            Next
                        End If

                    Case 3  'REVISIONE CANONE
                        par.cmd.CommandText = "delete from siscom_mi.canoni_ec_locatari where id_dichiarazione=" & idDichiarazione & " and id_contratto=(select id from siscom_mi.rapporti_utenza where cod_contratto='" & codcontratto.Value & "')"
                        par.cmd.ExecuteNonQuery()

                        If fl_contabilizzato = 1 Then
                            par.cmd.CommandText = "select importo from vsa_crediti_rid_canone WHERE id_domanda=" & idDomanda
                            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                            Dim dt As New Data.DataTable
                            da.Fill(dt)
                            da.Dispose()
                            If dt.Rows.Count > 0 Then
                                For Each row As Data.DataRow In dt.Rows
                                    par.cmd.CommandText = "delete from siscom_mi.bol_bollette_gest where id_tipo=56 and tipo_applicazione='N' and importo_totale=" & par.VirgoleInPunti(row.Item("importo")) & " and note like 'CONTAB. ANNO%' and id_contratto=(select id from siscom_mi.rapporti_utenza where cod_contratto='" & codcontratto.Value & "') "
                                    par.cmd.ExecuteNonQuery()
                                Next
                            End If

                            par.cmd.CommandText = "DELETE FROM vsa_crediti_rid_canone WHERE id_domanda=" & idDomanda
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "delete from siscom_mi.canoni_ec where id_dichiarazione=" & idDichiarazione & " and id_contratto=(select id from siscom_mi.rapporti_utenza where cod_contratto='" & codcontratto.Value & "')"
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "select canone from siscom_mi.canoni_ec where cod_contratto='" & codcontratto.Value & "' and data_calcolo = (select max (data_calcolo) from siscom_mi.canoni_ec ec where ec.id_contratto = canoni_ec.id_contratto)"
                            Dim myreaderC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myreaderC.Read Then
                                canone = par.IfNull(myreaderC("canone"), 0)
                            End If
                            myreaderC.Close()

                            par.cmd.CommandText = "select * from siscom_mi.rapporti_utenza where cod_contratto='" & codcontratto.Value & "' for update nowait"
                            Dim myreaderRU As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            myreaderRU.Close()

                            par.cmd.CommandText = "update siscom_mi.rapporti_utenza set imp_canone_iniziale=" & par.VirgoleInPunti(canone) & " where cod_contratto='" & codcontratto.Value & "'"
                            par.cmd.ExecuteNonQuery()
                        End If

                    Case 4  'CAMBIO ART.22 C.10 RR 1/2004
                        connData.chiudi(False)
                        Response.Write("<script>alert('Attenzione, non è possibile annullare un cambio art.22 autorizzato!');</script>")
                        Exit Sub
                    Case 5  'CAMBIO CONSENSUALE
                        connData.chiudi(False)
                        Response.Write("<script>alert('Attenzione, non è possibile annullare un cambio consensuale autorizzato!');</script>")
                        Exit Sub
                    Case 7  'OSPITALITA
                        par.cmd.CommandText = "select cod_fiscale from comp_nucleo_ospiti_vsa where id_domanda=" & idDomanda
                        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                        Dim dt As New Data.DataTable
                        da.Fill(dt)
                        da.Dispose()
                        If dt.Rows.Count > 0 Then
                            For Each row As Data.DataRow In dt.Rows
                                par.cmd.CommandText = "delete from siscom_mi.ospiti where cod_fiscale='" & row.Item("cod_fiscale") & "' and id_contratto=(select id from siscom_mi.rapporti_utenza where cod_contratto='" & codcontratto.Value & "')"
                                par.cmd.ExecuteNonQuery()
                            Next
                        End If
                    Case 8  'CREAZIONE POSIZIONE ABUSIVA
                        connData.chiudi(False)
                        Response.Write("<script>alert('Attenzione, non è possibile annullare il procedimento!');</script>")
                        Exit Sub
                    Case 9  'ABUSIVISMO AMM.VO ART.15 C.2 RR 1/2004
                        connData.chiudi(False)
                        Response.Write("<script>alert('Attenzione, non è possibile annullare il procedimento di abusivismo amm.vo');</script>")
                        Exit Sub
                    Case 10 'CAMBIO TIPOLOGIA CONTRATTUALE
                        connData.chiudi(False)
                        Response.Write("<script>alert('Attenzione, non è possibile procedere la posizione risulta aperta da altri operatori.');</script>")
                        Exit Sub
                End Select
            End If

            Response.Write("<script>alert('Operazione effettuata!');</script>")

            txtDataEvento.Text = ""
            txtDataFine.Text = ""
            txtDataIn.Text = ""
            txtDataPr.Text = ""
            txtPG.Text = ""

            connData.chiudi(True)

            btnProcedi.Visible = False

        Catch EX1 As Oracle.DataAccess.Client.OracleException

            If EX1.Number = 54 Then
                connData.chiudi(False)
                Response.Write("<script>alert('Attenzione, non è possibile procedere la posizione risulta aperta da altri operatori.');</script>")
            Else
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    connData.chiudi(False)
                End If
                Session.Add("ERRORE", "Provenienza: btnProcedi_Click - " & EX1.Message)
                lblErr.Visible = True
                lblErr.Text = EX1.Message
            End If

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: btnProcedi_Click - " & ex.Message)
            lblErr.Visible = True
            lblErr.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../../Contratti/pagina_home.aspx""</script>")
    End Sub
End Class
