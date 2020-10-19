
Partial Class ANAUT_RicercaApplicazioneAU
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Try

                par.RiempiDList(Me, par.OracleConn, "cmbFiliale", "select * from siscom_mi.tab_filiali where id_tipo_st=0 and acronimo is not null and id in (select id_filiale from siscom_mi.FILIALI_VIRTUALI) ORDER BY nome asc", "NOME", "ID")
                cmbFiliale.Items.Add(New ListItem("TUTTE", "-1"))
                cmbFiliale.Items.FindByText("TUTTE").Selected = True

                cmbNumCDa.Items.Add(New ListItem(" ", "-1"))
                cmbNumCDa.Items.Add(New ListItem("1", "1"))
                cmbNumCDa.Items.Add(New ListItem("2", "2"))
                cmbNumCDa.Items.Add(New ListItem("3", "3"))
                cmbNumCDa.Items.Add(New ListItem("4", "4"))
                cmbNumCDa.Items.Add(New ListItem("5", "5"))
                cmbNumCDa.Items.Add(New ListItem("6", "6"))
                cmbNumCDa.Items.Add(New ListItem("7", "7"))
                cmbNumCDa.Items.Add(New ListItem("8", "8"))
                cmbNumCDa.Items.Add(New ListItem("9", "9"))
                cmbNumCDa.Items.Add(New ListItem("10", "10"))
                cmbNumCDa.Items.Add(New ListItem("11", "11"))
                cmbNumCDa.Items.Add(New ListItem("12", "12"))
                cmbNumCDa.Items.FindByText(" ").Selected = True

                cmbNumCA.Items.Add(New ListItem(" ", "-1"))
                cmbNumCA.Items.Add(New ListItem("1", "1"))
                cmbNumCA.Items.Add(New ListItem("2", "2"))
                cmbNumCA.Items.Add(New ListItem("3", "3"))
                cmbNumCA.Items.Add(New ListItem("4", "4"))
                cmbNumCA.Items.Add(New ListItem("5", "5"))
                cmbNumCA.Items.Add(New ListItem("6", "6"))
                cmbNumCA.Items.Add(New ListItem("7", "7"))
                cmbNumCA.Items.Add(New ListItem("8", "8"))
                cmbNumCA.Items.Add(New ListItem("9", "9"))
                cmbNumCA.Items.Add(New ListItem("10", "10"))
                cmbNumCA.Items.Add(New ListItem("11", "11"))
                cmbNumCA.Items.Add(New ListItem("12", "12"))
                cmbNumCA.Items.FindByText(" ").Selected = True

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write(ex.Message)
            End Try
        End If

        txtStipulaDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtStipulaAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>self.close();</script>")
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click

        Dim bTrovato As Boolean
        Dim sValore As String
        Dim sCompara As String
        Dim sStringaSql As String = ""
        Dim sStringaSql1 As String = ""

        bTrovato = False
        sStringaSql = ""

        If cmbFiliale.SelectedItem.Value <> "-1" Then
            sValore = cmbFiliale.SelectedItem.Value
            sCompara = " = "
            bTrovato = True
            sStringaSql = sStringaSql & " tab_filiali.id " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If txtStipulaDal.Text <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = par.AggiustaData(txtStipulaDal.Text)
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_STIPULA>='" & par.PulisciStrSql(sValore) & "' "
        End If

        If txtStipulaAl.Text <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = par.AggiustaData(txtStipulaAl.Text)
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_STIPULA<='" & par.PulisciStrSql(sValore) & "' "
        End If

        If cmbNumCDa.SelectedItem.Value <> "-1" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = cmbNumCDa.SelectedItem.Value
            bTrovato = True
            sStringaSql = sStringaSql & " UTENZA_DICHIARAZIONI.N_COMP_NUCLEO>=" & sValore & " "
        End If

        If cmbNumCA.SelectedItem.Value <> "-1" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = cmbNumCA.SelectedItem.Value
            bTrovato = True
            sStringaSql = sStringaSql & " UTENZA_DICHIARAZIONI.N_COMP_NUCLEO<=" & sValore & " "
        End If

        If CheckBox1.Checked = True Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            bTrovato = True
            sStringaSql = sStringaSql & " UTENZA_DICHIARAZIONI.PRESENZA_MIN_15=1 "
        End If

        If CheckBox2.Checked = True Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            bTrovato = True
            sStringaSql = sStringaSql & " UTENZA_DICHIARAZIONI.PRESENZA_MAG_65=1 "
        End If

        If CheckBox3.Checked = True Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            bTrovato = True
            sStringaSql = sStringaSql & " UTENZA_DICHIARAZIONI.N_INV_100_CON>=1 "
        End If

        If CheckBox4.Checked = True Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            bTrovato = True
            sStringaSql = sStringaSql & " UTENZA_DICHIARAZIONI.N_INV_100_SENZA>=1 "
        End If

        If CheckBox5.Checked = True Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            bTrovato = True
            sStringaSql = sStringaSql & " UTENZA_DICHIARAZIONI.N_INV_100_66>=1 "
        End If

        If CheckBox6.Checked = True Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            bTrovato = True
            sStringaSql = sStringaSql & " UTENZA_DICHIARAZIONI.PREVALENTE_DIP=1 "
        End If




        Dim dt As New Data.DataTable
        dt = CType(HttpContext.Current.Session.Item("ElencoOriginaleDT"), Data.DataTable)
        Dim s As String = ""

        If Not IsNothing(dt) Then
            For Each r As Data.DataRow In dt.Rows
                s = s & r.Item("IDC") & ","
            Next
            If s <> "" Then
                s = "AND RAPPORTI_UTENZA.ID NOT IN (" & Mid(s, 1, Len(s) - 1) & ")"
            End If
        End If

        s = ""
        dt = CType(HttpContext.Current.Session.Item("ElencoRegistroDT"), Data.DataTable)
        If Not IsNothing(dt) Then
            For Each r As Data.DataRow In dt.Rows
                s = s & r.Item("IDC") & ","
            Next
            If s <> "" Then
                s = "AND RAPPORTI_UTENZA.ID NOT IN (" & Mid(s, 1, Len(s) - 1) & ")"
            End If
        End If

        sStringaSql1 = "SELECT " _
                    & "UTENZA_DICHIARAZIONI.ID AS IDAU,UTENZA_DICHIARAZIONI.pg AS PG_AU,UTENZA_DICHIARAZIONI.N_COMP_NUCLEO,UTENZA_DICHIARAZIONI.N_INV_100_CON,UTENZA_DICHIARAZIONI.N_INV_100_SENZA,UTENZA_DICHIARAZIONI.N_INV_100_66 as n_inv_66_99," _
                    & "DECODE(PREVALENTE_DIP,0,'NO',1,'SI') AS PREVALENTE,DECODE(PRESENZA_MIN_15,0,'NO',1,'SI') AS PRESENZA_15,DECODE(PRESENZA_MAG_65,0,'NO',1,'SI') AS PRESENZA_65, RAPPORTI_UTENZA.ID AS IDC,COD_CONTRATTO,ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,COD_TIPOLOGIA_CONTR_LOC AS TIPOLOGIA," _
                    & "TO_CHAR(TO_DATE(RAPPORTI_UTENZA.DATA_DECORRENZA,'YYYYmmdd'),'DD/MM/YYYY') AS DECORRENZA,TO_CHAR(TO_DATE(DATA_RICONSEGNA,'YYYYmmdd'),'DD/MM/YYYY') AS SCADENZA, " _
                    & "INDIRIZZI.DESCRIZIONE AS INDIRIZZO_UNITA,INDIRIZZI.CIVICO AS CIVICO_UNITA,INDIRIZZI.CAP AS CAP_UNITA,INDIRIZZI.LOCALITA AS COMUNE_UNITA,(TAB_FILIALI.NOME ) AS FILIALE  " _
                    & "FROM siscom_mi.unita_contrattuale, SISCOM_MI.ANAGRAFICA, SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.INDIRIZZI, SISCOM_MI.UNITA_IMMOBILIARI, siscom_mi.TAB_FILIALI," _
                    & "siscom_mi.COMPLESSI_IMMOBILIARI, siscom_mi.EDIFICI, UTENZA_DICHIARAZIONI " _
                    & "WHERE " _
                    & " NVL(FL_GENERAZ_AUTO,0)=0 AND UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA<>'VEND' AND (UTENZA_DICHIARAZIONI.NOTE_WEB IS NULL OR UTENZA_DICHIARAZIONI.NOTE_WEB<>'GENERATA_AUTOMATICAMENTE') AND ((UTENZA_DICHIARAZIONI.FL_SOSP_7=1 AND UTENZA_DICHIARAZIONI.FL_SOSP_1=0 AND UTENZA_DICHIARAZIONI.FL_SOSP_2=0 AND UTENZA_DICHIARAZIONI.FL_SOSP_3=0 AND UTENZA_DICHIARAZIONI.FL_SOSP_4=0 AND UTENZA_DICHIARAZIONI.FL_SOSP_5=0 AND UTENZA_DICHIARAZIONI.FL_SOSP_6=0) " _
                    & "Or (UTENZA_DICHIARAZIONI.FL_SOSP_7 = 0 And UTENZA_DICHIARAZIONI.FL_SOSP_1 = 0 And UTENZA_DICHIARAZIONI.FL_SOSP_2 = 0 And UTENZA_DICHIARAZIONI.FL_SOSP_3 = 0 And UTENZA_DICHIARAZIONI.FL_SOSP_4 = 0 And UTENZA_DICHIARAZIONI.FL_SOSP_5 = 0 And UTENZA_DICHIARAZIONI.FL_SOSP_6 = 0))  AND UTENZA_DICHIARAZIONI.ID_BANDO=2 AND " _
                    & "UTENZA_DICHIARAZIONI.RAPPORTO IS NOT NULL AND UTENZA_DICHIARAZIONI.RAPPORTO=RAPPORTI_UTENZA.COD_CONTRATTO AND UTENZA_DICHIARAZIONI.ID NOT IN " _
                    & "(SELECT ID_DICHIARAZIONE FROM UTENZA_GRUPPI_DICHIARAZIONI) AND SUBSTR(COD_CONTRATTO,1,2)<>'41' AND SUBSTR(COD_CONTRATTO,1,2)<>'42' AND SUBSTR(COD_CONTRATTO,1,2)<>'43' AND " _
                    & "SUBSTR(COD_CONTRATTO,1,6)<>'000000' AND COMPLESSI_IMMOBILIARI.id_filiale=TAB_FILIALI.ID (+) AND COMPLESSI_IMMOBILIARI.ID=EDIFICI.id_complesso AND EDIFICI.ID=UNITA_IMMOBILIARI.id_edificio " _
                    & " AND (anagrafica.ragione_sociale IS NULL OR anagrafica.ragione_sociale='') AND  ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID " _
                    & "AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE'   AND INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID=unita_contrattuale.id_unita AND " _
                    & "unita_contrattuale.id_contratto=rapporti_utenza.ID AND unita_contrattuale.id_unita_principale IS NULL  AND  COD_CONTRATTO IS NOT NULL " & s



        If sStringaSql <> "" Then
            sStringaSql1 = sStringaSql1 & " AND " & sStringaSql
        End If
        sStringaSql1 = sStringaSql1 & " ORDER BY descrizione asc,indirizzi.civico asc,anagrafica.cognome ASC,anagrafica.nome asc"
        Session.Add("PGAPPLICAZIONEAU", sStringaSql1)
        Response.Redirect("RisultatoApplicazioneAU.aspx")
        'Response.Write("<script>location.replace('RisultatoApplicazioneAU.aspx');</script>")

    End Sub
End Class
