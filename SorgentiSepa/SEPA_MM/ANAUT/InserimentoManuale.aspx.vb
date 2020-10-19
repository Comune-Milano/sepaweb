
Partial Class ANAUT_InserimentoManuale
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Dim Tipo1 As Integer = 0
    Dim Tipo2 As Integer = 0
    Dim Tipo3 As Integer = 0
    Dim Tipo4 As Integer = 0
    Dim Tipo5 As Integer = 0
    Dim Tipo6 As Integer = 0
    Dim Tipo7 As Integer = 0
    Dim Tipo8 As Integer = 0
    Dim Tipo9 As Integer = 0
    Dim Tipo10 As Integer = 0
    Dim Tipo11 As Integer = 0


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Trim(Session.Item("OPERATORE"))) Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

        End If
    End Sub

    Protected Sub btnCerca_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        rdElenco.Items.Clear()
        lblDati.Visible = False
        If txtCognome.Text = "" And txtNome.Text = "" And txtContratto.Text = "" Then
            lblDati.Visible = True
            lblDati.Text = "Attenzione...Specificare almeno un criterio di ricerca tra quelli possibili."
            Exit Sub
        End If
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim INDICEBANDO As Long = 0
            Dim INIZIO_VALIDITA As String = ""
            Dim ss As String = "("

            par.cmd.CommandText = "SELECT * FROM UTENZA_BANDI WHERE stato=1 order by id desc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                INDICEBANDO = par.IfNull(myReader("ID"), 0)
                INIZIO_VALIDITA = par.IfNull(myReader("INIZIO_CANONE"), "")
                Tipo1 = par.IfNull(myReader("ERP_1"), 0) 'ERP SOCIALE
                Tipo2 = par.IfNull(myReader("ERP_2"), 0) 'ERP MODERATO
                Tipo3 = par.IfNull(myReader("ERP_ART_22"), 0) 'ART 200 C 10
                Tipo4 = par.IfNull(myReader("ERP_4"), 0)
                Tipo5 = par.IfNull(myReader("ERP_5"), 0)
                Tipo10 = par.IfNull(myReader("ERP_3"), 0)
                Tipo6 = par.IfNull(myReader("L43198"), 0)
                Tipo7 = par.IfNull(myReader("L39278"), 0)
                Tipo8 = par.IfNull(myReader("ERP_FF_OO"), 0) 'FF.OO.
                Tipo9 = par.IfNull(myReader("ERP_CONV"), 0) 'ERP CONVENZIONATO
                Tipo11 = par.IfNull(myReader("OA"), 0)

            End If
            myReader.Close()

            Dim sStringa As String = ""
            Dim b As Boolean = False
            Dim V As String = ""
            Dim sValore As String = ""
            Dim sCompara As String = ""

            If txtCognome.Text <> "" Then

                sValore = txtCognome.Text
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                If b = True Then sStringa = sStringa & " AND "
                sStringa = sStringa & " UPPER(ANAGRAFICA.COGNOME)" & sCompara & " '" & UCase(par.PulisciStrSql(sValore)) & "' "
                b = True
            End If

            If txtNome.Text <> "" Then
                sValore = txtNome.Text
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                If b = True Then sStringa = sStringa & " AND "
                sStringa = sStringa & " UPPER(ANAGRAFICA.NOME)" & sCompara & " '" & UCase(par.PulisciStrSql(sValore)) & "' "
                b = True
            End If

            If txtContratto.Text <> "" Then
                If b = True Then sStringa = sStringa & " AND "
                sStringa = sStringa & " UPPER(RAPPORTI_UTENZA.COD_CONTRATTO)='" & UCase(par.PulisciStrSql(txtContratto.Text)) & "' "
                b = True
            End If

            If Tipo1 = 1 Then 'erp sociale
                ss = ss & " (rapporti_utenza.provenienza_ass = 1 AND unita_immobiliari.id_destinazione_uso <> 2) or (rapporti_utenza.provenienza_ass = 2 AND unita_immobiliari.id_destinazione_uso <> 2) or "
            End If

            If Tipo2 = 1 Then 'erp moderato
                ss = ss & " unita_immobiliari.id_destinazione_uso = 2 or "
            End If

            If Tipo3 = 1 Then 'ART 22 C 10
                ss = ss & " rapporti_utenza.provenienza_ass = 8 or "
            End If

            If Tipo8 = 1 Then 'FF.OO.
                ss = ss & " rapporti_utenza.provenienza_ass = 10 or "
            End If

            If Tipo9 = 1 Then 'convenzionato
                ss = ss & " unita_immobiliari.id_destinazione_uso = 12 or "
            End If

            If Tipo6 = 1 Then
                ss = ss & " rapporti_utenza.dest_uso = 'P' or rapporti_utenza.dest_uso = 'S' OR rapporti_utenza.dest_uso = '0' or "
            End If

            If Tipo7 = 1 Then
                ss = ss & " (rapporti_utenza.provenienza_ass = 5 AND unita_immobiliari.cod_tipologia='AL')  or "
            End If

            If Tipo10 = 1 Then
                ss = ss & " rapporti_utenza.dest_uso = 'D' OR "
            End If

            If Tipo4 = 1 And ss = "(" Then
                ss = ss & "rapporti_utenza.dest_uso='X' OR "
            End If

            If Tipo5 = 1 And ss = "(" Then
                ss = ss & "rapporti_utenza.dest_uso='X' OR "
            End If

            If Tipo11 = 1 Then 'OCCUPAZIONI ABUSIVE
                ss = ss & " rapporti_utenza.provenienza_ass = 7 or "
            End If

            'max caricamento 431/98 senza poi applicare au
            ss = ss & " rapporti_utenza.provenienza_ass = 6 or "

            If ss = "(" Then
                ss = "(rapporti_utenza.dest_uso='X') "
            Else
                ss = Mid(ss, 1, Len(ss) - 4) & ") AND "
            End If

            Dim I As Long = 0
            'par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.ID,COD_CONTRATTO,ANAGRAFICA.NOME,ANAGRAFICA.COGNOME,ANAGRAFICA.COD_FISCALE FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI WHERE " & ss & " UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA<>'VEND' AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND RAPPORTI_UTENZA.DATA_RICONSEGNA IS NULL AND RAPPORTI_UTENZA.ID_TIPO_CONTRATTO IN (SELECT ID FROM TIPI_CONTRATTO_LOCAZIONE WHERE ID_DESTINAZIONE_USO=1) AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM CONVOCAZIONI_AU WHERE ID_CONTRATTO IS NOT NULL AND ID_GRUPPO IN (SELECT ID FROM CONVOCAZIONI_AU_GRUPPI WHERE ID_AU=" & INDICEBANDO & ")) AND " & sStringa & " ORDER BY COGNOME,NOME"
            par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.ID,COD_CONTRATTO,ANAGRAFICA.NOME,ANAGRAFICA.COGNOME,ANAGRAFICA.COD_FISCALE,RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI WHERE " & ss & " UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA<>'VEND' AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND NVL(RAPPORTI_UTENZA.DATA_RICONSEGNA,'29991231')>='" & INIZIO_VALIDITA & "' AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.CONVOCAZIONI_AU WHERE ID_CONTRATTO IS NOT NULL AND ID_GRUPPO IN (SELECT ID FROM SISCOM_MI.CONVOCAZIONI_AU_GRUPPI WHERE ID_AU=" & INDICEBANDO & ")) AND " & sStringa & " ORDER BY COGNOME,NOME"
            myReader = par.cmd.ExecuteReader()
            Do While myReader.Read
                rdElenco.Items.Add(New ListItem(par.IfNull(myReader("COD_CONTRATTO"), "") & "-" & par.IfNull(myReader("COD_TIPOLOGIA_CONTR_LOC"), "") & " " & "-" & par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "") & " - " & par.IfNull(myReader("COD_FISCALE"), ""), par.IfNull(myReader("ID"), "-1")))

            Loop

            If rdElenco.Items.Count = 0 Then
                lblDati.Visible = True
                lblDati.Text = "Attenzione...Nessun contratto trovato."
                btnProcedi.Visible = False
            Else
                btnProcedi.Visible = True
            End If

            myReader.Close()


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            lblDati.Visible = True
            lblDati.Text = ex.Message
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click

        Dim i As Long = 0
        Dim idc As Long = 0

        For i = 0 To rdElenco.Items.Count - 1
            If rdElenco.Items(i).Selected = True Then
                idc = rdElenco.Items(i).Value
            End If
        Next
        If idc <> 0 Then
            Try
                Dim INDICEBANDO As Long = 0
                Dim COGNOME As String = ""
                Dim NOME As String = ""

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                par.cmd.CommandText = "SELECT * FROM UTENZA_BANDI WHERE stato=1 order by id desc"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    INDICEBANDO = par.IfNull(myReader("ID"), 0)
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CONVOCAZIONI_AU WHERE ID_GRUPPO IN (SELECT ID FROM SISCOM_MI.CONVOCAZIONI_AU_GRUPPI WHERE ID_AU=" & INDICEBANDO & ") AND ID_CONTRATTO=(SELECT ID FROM SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO='" & UCase(txtContratto.Text) & "')"
                myReader = par.cmd.ExecuteReader()
                If myReader.HasRows = True Then

                    lblDati.Visible = True
                    lblDati.Text = "Attenzione...questo contratto è stato convocato per l'attuale AU."
                Else
                    par.cmd.CommandText = "select ANAGRAFICA.COGNOME,ANAGRAFICA.NOME FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND RAPPORTI_UTENZA.id=" & idc
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        COGNOME = par.IfNull(myReader1("COGNOME"), "")
                        NOME = par.IfNull(myReader1("NOME"), "")
                    End If
                    myReader1.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CONVOCAZIONI_AU WHERE ID_GRUPPO IN (SELECT ID FROM SISCOM_MI.CONVOCAZIONI_AU_GRUPPI WHERE ID_AU=" & INDICEBANDO & ") AND ID_CONTRATTO is null order by id asc"
                    myReader1 = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        par.cmd.CommandText = "UPDATE SISCOM_MI.AGENDA_APPUNTAMENTI SEt COD_CONTRATTO='" & UCase(txtContratto.Text) & "',ID_CONTRATTO=" & idc & ",COGNOME='" & par.PulisciStrSql(COGNOME) & "',NOME='" & par.PulisciStrSql(NOME) & "' WHERE ID_convocazione=" & myReader1("ID")
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "UPDATE SISCOM_MI.CONVOCAZIONI_AU SEt ID_CONTRATTO=" & idc & ",COGNOME='" & par.PulisciStrSql(COGNOME) & "',NOME='" & par.PulisciStrSql(NOME) & "' WHERE ID_CONTRATTO IS NULL AND ID=" & myReader1("ID")
                        par.cmd.ExecuteNonQuery()

                        Response.Write("<script>alert('Operazione effettuata!');location.replace('SchedaAppuntamento.aspx?X=0&ID=" & par.CriptaMolto(myReader1("ID")) & "');</script>")
                    End If
                End If


                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Catch ex As Exception
                lblDati.Visible = True
                lblDati.Text = ex.Message
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End Try
        Else
            lblDati.Visible = True
            lblDati.Text = "Attenzione...Selezionare un contratto dalla lista"
        End If
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub
End Class
