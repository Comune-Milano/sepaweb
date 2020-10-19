' REPORT FORNITORI 

Partial Class ReportFornitoriG
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Dim vIdFornitore As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StringaSql As String

        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then


            vIdFornitore = Request.QueryString("ID_FORNITORE")

            If IsNumeric(vIdFornitore) Then

                Try
                    ' LEGGO FORNITORE
                    Label2.Text = "FORNITORE GIURIDICO"

                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    par.cmd.CommandText = "select * from SISCOM_MI.FORNITORI where SISCOM_MI.FORNITORI.ID=" & vIdFornitore
                    myReader1 = par.cmd.ExecuteReader()

                    If myReader1.Read Then
                        lblragione.Text = par.IfNull(myReader1("RAGIONE_SOCIALE"), "")
                        lbliva.Text = par.IfNull(myReader1("PARTITA_IVA"), "")
                        lblcognome.Text = par.IfNull(myReader1("COGNOME"), "")
                        lblnome.Text = par.IfNull(myReader1("NOME"), "")
                        lblCF.Text = par.IfNull(myReader1("COD_FISCALE"), "")
                        lblTel.Text = par.IfNull(myReader1("NUM_TELEFONO"), "")
                        lblcomune.Text = par.IfNull(myReader1("COMUNE_RESIDENZA"), "")
                        lblpr.Text = par.IfNull(myReader1("PR_RESIDENZA"), "")
                        lblindirizzo.Text = par.IfNull(myReader1("TIPO_INDIRIZZO_RESIDENZA"), "") & " " & par.IfNull(myReader1("INDIRIZZO_RESIDENZA"), "")
                        lblcivico.Text = par.IfNull(myReader1("CIVICO_RESIDENZA"), "")
                        lblcap.Text = par.IfNull(myReader1("CAP_RESIDENZA"), "")
                        lbliban.Text = par.IfNull(myReader1("IBAN"), "")
                        lblcfr.Text = par.IfNull(myReader1("COD_FISCALE_R"), "")
                        lblcomunea.Text = par.IfNull(myReader1("COMUNE_SEDE_A"), "")
                        lblcomuner.Text = par.IfNull(myReader1("COMUNE_RESIDENZA_R"), "")
                        lblpra.Text = par.IfNull(myReader1("PR_SEDE_A"), "")
                        lblprr.Text = par.IfNull(myReader1("PR_RESIDENZA_R"), "")
                        lblindirizzoa.Text = par.IfNull(myReader1("TIPO_INDIRIZZO_SEDE_A"), "") & " " & par.IfNull(myReader1("INDIRIZZO_SEDE_A"), "")
                        lblindirizzor.Text = par.IfNull(myReader1("TIPO_INDIRIZZO_RESIDENZA_R"), "") & par.IfNull(myReader1("INDIRIZZO_RESIDENZA_R"), "")
                        lblcivicoa.Text = par.IfNull(myReader1("CIVICO_SEDE_A"), "")
                        lblcivicor.Text = par.IfNull(myReader1("CIVICO_RESIDENZA_R"), "")
                        lblfax.Text = par.IfNull(myReader1("NUM_FAX"), "")
                        lblfaxa.Text = par.IfNull(myReader1("NUM_FAX_SEDE_A"), "")
                        lblcapa.Text = par.IfNull(myReader1("CAP_SEDE_A"), "")
                        lblcapr.Text = par.IfNull(myReader1("CAP_RESIDENZA_R"), "")
                        lbltela.Text = par.IfNull(myReader1("NUM_TELEFONO_SEDE_A"), "")
                        lbltelr.Text = par.IfNull(myReader1("TELEFONO_R"), "")
                        lblprocura.Text = par.IfNull(myReader1("NUM_PROCURA"), "")
                        lbldataprocura.Text = par.FormattaData(par.IfNull(myReader1("DATA_PROCURA"), ""))
                        If myReader1("TIPO_R") = "P" Then
                            lbltipor.Text = "PROCURATORE LEG."
                        Else
                            lbltipor.Text = "LEGALE RAPP."
                        End If
                        lblNote.Text = par.IfNull(myReader1("RIFERIMENTI"), "")

                    End If

                    '*** ELENCO APPALTI
                    StringaSql = "  select SISCOM_MI.APPALTI.ID, SISCOM_MI.APPALTI.NUM_REPERTORIO, TO_CHAR(TO_DATE(SISCOM_MI.APPALTI.DATA_REPERTORIO,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_REPERTORIO"", SISCOM_MI.APPALTI.ID_TIPOLOGIA AS ""ID_TIPO"", SISCOM_MI.TIPOLOGIA_APPALTI.DESCRIZIONE AS ""TIPO"", (CASE (APPALTI.SAL) WHEN 0 THEN 'NO' ELSE 'SI' END) AS ""SAL"", SISCOM_MI.APPALTI.DESCRIZIONE, SISCOM_MI.APPALTI.ANNO_INIZIO, SISCOM_MI.APPALTI.DURATA_MESI AS ""DURATA"", SISCOM_MI.APPALTI.BASE_ASTA_CANONE AS ""ASTA_CANONE"", SISCOM_MI.APPALTI.BASE_ASTA_CONSUMO AS ""ASTA_CONSUMO"", " _
                & "SISCOM_MI.APPALTI.ONERI_SICUREZZA_CANONE AS ""ONERI_CANONE"", SISCOM_MI.APPALTI.ONERI_SICUREZZA_CONSUMO AS ""ONERI_CONSUMO"", SISCOM_MI.APPALTI.PERC_ONERI_SICUREZZA AS ""ONERI"", SISCOM_MI.APPALTI.PENALI, SISCOM_MI.APPALTI.ANNO_RIF_INIZIO AS ""RIFINIZIO"", SISCOM_MI.APPALTI.ANNO_RIF_FINE AS ""RIFINE"", SISCOM_MI.APPALTI.IVA_CANONE, SISCOM_MI.APPALTI.IVA_CONSUMO, SISCOM_MI.APPALTI.COSTO_GRADO_GIORNO AS ""COSTO"", " _
                & "SISCOM_MI.LOTTI_SERVIZI.ID AS ""ID_LOTTO"", SISCOM_MI.LOTTI_SERVIZI.DESCRIZIONE AS ""DESCRIZIONE_LOTTO"", SISCOM_MI.TAB_SERVIZI.DESCRIZIONE AS ""SERVIZIO"", SISCOM_MI.APPALTI.PERC_SCONTO_CANONE AS ""SCONTO_CANONE"", SISCOM_MI.APPALTI.PERC_SCONTO_CONSUMO AS ""SCONTO_CONSUMO""" _
                            & " from SISCOM_MI.APPALTI, SISCOM_MI.LOTTI_SERVIZI, SISCOM_MI.TIPOLOGIA_APPALTI,SISCOM_MI.TAB_SERVIZI" _
            & " where SISCOM_MI.APPALTI.ID_FORNITORE = " & vIdFornitore & " and " _
          & "SISCOM_MI.APPALTI.ID=SISCOM_MI.LOTTI_SERVIZI.ID_APPALTO (+) AND " _
         & "SISCOM_MI.APPALTI.ID_TIPOLOGIA=SISCOM_MI.TIPOLOGIA_APPALTI.ID (+) AND " _
         & " SISCOM_MI.LOTTI_SERVIZI.ID_SERVIZIO=SISCOM_MI.TAB_SERVIZI.ID (+)  " _
         & "order by SISCOM_MI.APPALTI.NUM_REPERTORIO "


                    par.cmd.CommandText = StringaSql

                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim ds As New Data.DataSet()

                    da.Fill(ds, "APPALTI")

                    DataGrid1.DataSource = ds
                    DataGrid1.DataBind()

                    ds.Dispose()
                    '*************************

                    '*** EVENTI_IMPIANTI
                    InserisciEvento(par.cmd, vIdFornitore, Session.Item("ID_OPERATORE"), 90, "Stampa fornitori e appalti assegnati")


                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    par.OracleConn.Dispose()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Catch EX1 As Oracle.DataAccess.Client.OracleException
                    par.OracleConn.Close()
                    par.OracleConn.Dispose()
                Catch ex As Exception
                    par.OracleConn.Close()
                    par.OracleConn.Dispose()
                End Try
            End If
        End If

    End Sub

    Public Function InserisciEvento(ByVal MyPar As Oracle.DataAccess.Client.OracleCommand, ByVal vIdFornitore As Long, ByVal vIdOperatore As Long, ByVal Tab_Eventi As Integer, ByRef Motivazione As String) As Boolean

        Try

            InserisciEvento = False

            MyPar.Parameters.Clear()

            MyPar.CommandText = "insert into SISCOM_MI.EVENTI_FORNITORI (ID_FORNITORE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                & "VALUES (" & vIdFornitore & "," & vIdOperatore & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                & "'F" & Tab_Eventi & "','" & par.PulisciStrSql(Motivazione) & "')"
            MyPar.ExecuteNonQuery()
            MyPar.CommandText = ""
            MyPar.Parameters.Clear()


        Catch ex As Exception
            InserisciEvento = False
            MyPar.Parameters.Clear()
        End Try

    End Function

End Class
