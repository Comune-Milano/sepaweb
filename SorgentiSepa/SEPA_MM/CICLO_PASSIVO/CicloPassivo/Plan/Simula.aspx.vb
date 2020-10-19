
Partial Class Contabilita_CicloPassivo_Plan_Simula
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim DTEdifici As New Data.DataTable
    Dim DTUnita As New Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0

        If IsPostBack = False Then
            Carica(Request.QueryString("IDPF"))
        End If

    End Sub

    Function Carica(ByVal IdPianoF As String)
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim i As Long

            'MEMORIZZO TUTTI GLI EDIFICI CON RELATIVO LOTTO DEL PIANO FINANZIARIO
            DTEdifici.Columns.Add("ID_EDIFICIO")
            DTEdifici.Columns.Add("ID_LOTTO")
            DTEdifici.Columns.Add("NUM_UNITA")
            DTEdifici.Columns.Add("IMPORTO")
            DTEdifici.Columns.Add("ID_SERVIZIO")
            Dim RIGA As System.Data.DataRow

            par.cmd.CommandText = "select * from siscom_mi.lotti_patrimonio where id_complesso is null and id_lotto in (select id_lotto from siscom_mi.pf_voci_importo where id_voce in (select id from siscom_mi.pf_voci where id_piano_finanziario=" & idPianoF & "))"
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader5.Read
                RIGA = DTEdifici.NewRow()
                RIGA.Item("ID_EDIFICIO") = par.IfNull(myReader5("ID_EDIFICIO"), "-1")
                RIGA.Item("ID_LOTTO") = par.IfNull(myReader5("ID_LOTTO"), "-1")
                RIGA.Item("NUM_UNITA") = "0"
                RIGA.Item("IMPORTO") = "0"
                RIGA.Item("ID_SERVIZIO") = "0"
                DTEdifici.Rows.Add(RIGA)
            Loop
            myReader5.Close()

            par.cmd.CommandText = "select lotti_patrimonio.id_lotto,edifici.id from siscom_mi.edifici,siscom_mi.lotti_patrimonio where lotti_patrimonio.id_complesso=edifici.id_complesso and edifici.id_complesso in (select id_complesso from siscom_mi.lotti_patrimonio where  id_complesso is not null and id_lotto in (select id_lotto from siscom_mi.pf_voci_importo where id_voce in (select id from siscom_mi.pf_voci where id_piano_finanziario=" & IdPianoF & ")))"
            myReader5 = par.cmd.ExecuteReader()
            Do While myReader5.Read
                RIGA = DTEdifici.NewRow()
                RIGA.Item("ID_EDIFICIO") = par.IfNull(myReader5("ID"), "-1")
                RIGA.Item("ID_LOTTO") = par.IfNull(myReader5("ID_LOTTO"), "-1")
                RIGA.Item("NUM_UNITA") = "0"
                RIGA.Item("IMPORTO") = "0"
                RIGA.Item("ID_SERVIZIO") = "0"
                DTEdifici.Rows.Add(RIGA)
            Loop
            myReader5.Close()

            lblNumEdifici.Text = "Numero Edifici interessati nel nel Piano Finanziario: " & DTEdifici.Rows.Count




            'CREO UNA DATATABLE CHE SARA RIEMPITA CON ID_UNITA + SERVIZIO + IMPORTO PER QUEL SERVIZIO
            DTUnita.Columns.Add("ID")
            DTUnita.Columns.Add("ID_SERVIZIO")
            DTUnita.Columns.Add("MILLESIMI")
            DTUnita.Columns.Add("IMPORTO")

            Dim RIGA1 As System.Data.DataRow


            Dim Tot As Double = 0
            Dim NumUnitaLotto As Long = 0
            Dim Moltiplicatore As Double = 0


            'PRENDO TUTTI I SERVIZI
            par.cmd.CommandText = "select * from siscom_mi.tab_servizi order by id asc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader.Read
                Tot = 0
                NumUnitaLotto = 0
                'PRENDO TUTTI I LOTTI DI UN SERVIZIO DI UN DETERMINATO PIANO FINANZIARIO
                par.cmd.CommandText = "select * from siscom_mi.lotti_servizi where id_servizio=" & myReader("id") & " and id in (select id_lotto from siscom_mi.pf_voci_importo where id_voce in (select id from siscom_mi.pf_voci where id_piano_finanziario=" & IdPianoF & "))"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReader1.Read

                    'PER OGNI LOTTO PRENDO L'IMPORTO + IVA, IN RAPPORTO ALLA REVERSIBILITA
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE VALORE>0 AND PERC_REVERSIBILITA>0 AND ID_LOTTO=" & myReader1("ID")
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    Do While myReader2.Read
                        Tot = Tot + (par.IfNull(myReader2("perc_reversibilita"), 0) * ((par.IfNull(myReader2("valore"), 0) + (par.IfNull(myReader2("valore"), 0) * (20 / 100))))) / 100
                    Loop
                    myReader2.Close()

                    'CONTO IL NUMERO DELLE UNITA DEL LOTTO CHE HANNO DEI MILLESIMI PER IL SERVIZIO IN OGGETTO
                    For i = 0 To DTEdifici.Rows.Count - 1
                        If DTEdifici.Rows(i).Item(1).ToString = myReader1("ID") Then
                            'PER PROVA PRENDO TUTTE LE UNITA VISTO CHE MANCANO LE TABELLE MILLESIMALI
                            par.cmd.CommandText = "select count(unita_immobiliari.id) from siscom_mi.unita_immobiliari where id_edificio=" & DTEdifici.Rows(i).Item(0).ToString '& " and unita_immobiliari.id in (select id_unita_immobiliare from valori_millesimali)"

                            'par.cmd.CommandText = "select count(unita_immobiliari.id) from siscom_mi.unita_immobiliari where id_edificio=" & DTEdifici.Rows(i).Item(0).ToString & " and unita_immobiliari.id in (select id_unita_immobiliare from valori_millesimali where id_tabella in (select id from siscom_mi.tabelle_millesimali where cod_tipologia in (select cod from siscom_mi.tipologia_millesimale where id_servizio=" & myReader("id") & ")))"
                            myReader5 = par.cmd.ExecuteReader()
                            If myReader5.Read Then
                                NumUnitaLotto = NumUnitaLotto + par.IfNull(myReader5(0), 0)
                                DTEdifici.Rows(i).Item(2) = par.IfNull(myReader5(0), 0)
                            End If
                            myReader5.Close()
                        End If
                    Next

                    If NumUnitaLotto <= 0 Then
                        NumUnitaLotto = 1
                    End If




                    'DIVIDO L'IMPORTO DEL LOTTO PER IL NUMERO DELLE UNITA DEL LOTTO
                    Moltiplicatore = Tot / NumUnitaLotto

                    'PER OGNI EDIFICIO DEL LOTTO, METTO L'IMPORTO IN BASE AL NUMERO UNITA E AL MOLTIPLICATORE
                    For i = 0 To DTEdifici.Rows.Count - 1
                        If DTEdifici.Rows(i).Item(1).ToString = myReader1("ID") Then
                            'PRENDO L'IMPORTO DEL SINGOLO EDIFICIO DA DATABASE
                            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.lotti_patrimonio_importi WHERE id_edificio=" & DTEdifici.Rows(i).Item(0).ToString & " and iD_LOTTO=" & DTEdifici.Rows(i).Item(1).ToString
                            myReader5 = par.cmd.ExecuteReader()
                            If myReader5.HasRows = True Then
                                If myReader5.Read Then
                                    DTEdifici.Rows(i).Item(3) = par.IfNull(myReader5("importo"), 0)
                                    DTEdifici.Rows(i).Item(4) = myReader("id")
                                End If
                            Else
                                'SE SONO A LIVELLO DI COMPLESSO...CHE FACCIO??

                                'DTEdifici.Rows(i).Item(3) = Moltiplicatore * DTEdifici.Rows(i).Item(2)
                                'DTEdifici.Rows(i).Item(4) = myReader("id")
                            End If
                            myReader5.Close()

                        End If
                    Next







                    'RIGA1 = DTUnita.NewRow()

                    'RIGA1.Item("ID") = par.IfNull(myReader5("CAP"), "")
                    'RIGA1.Item("ID_SERVIZIO") = par.IfNull(myReader5("CAP"), "")
                    'RIGA1.Item("IMPORTO") = par.IfNull(myReader5("CAP"), "")

                    'DTUnita.Rows.Add(RIGA1)

                Loop
                myReader1.Close()

                'RIGA = DTEdifici.NewRow()
                'RIGA.Item("ID_EDIFICIO") = par.IfNull(myReader5("ID_EDIFICIO"), "-1")
                'DTEdifici.Rows.Add(RIGA)
            Loop
            myReader.Close()

            Dim ImpDaDividere As Double = 0

            For i = 0 To DTEdifici.Rows.Count - 1

                ImpDaDividere = CDbl(DTEdifici.Rows(i).Item(3).ToString)

                ''PER PROVA PRENDO TUTTE LE UNITA VISTO CHE MANCANO LE TABELLE MILLESIMALI
                'par.cmd.CommandText = "select * from siscom_mi.unita_immobiliari where id_edificio=" & DTEdifici.Rows(i).Item(0).ToString '& " and unita_immobiliari.id in (select id_unita_immobiliare from valori_millesimali)"
                par.cmd.CommandText = "select valori_millesimali.valore_millesimo,unita_immobiliari.* from siscom_mi.valori_millesimali,siscom_mi.unita_immobiliari where unita_immobiliari.id=valori_millesimali.id_unita_immobiliare (+) and  unita_immobiliari.id_edificio=" & DTEdifici.Rows(i).Item(0).ToString & " and unita_immobiliari.id in (select id_unita_immobiliare from siscom_mi.valori_millesimali where id_tabella in (select id from siscom_mi.tabelle_millesimali where cod_tipologia in (select cod from siscom_mi.tipologia_millesimale where id_servizio=" & DTEdifici.Rows(i).Item(4).ToString & "))) order by unita_immobiliari.cod_unita_immobiliare asc"
                myReader5 = par.cmd.ExecuteReader()
                Do While myReader5.Read
                    RIGA1 = DTUnita.NewRow()

                    RIGA1.Item("ID") = par.IfNull(myReader5("ID"), "")
                    RIGA1.Item("ID_SERVIZIO") = DTEdifici.Rows(i).Item(4).ToString
                    RIGA1.Item("MILLESIMI") = par.IfNull(myReader5("VALORE_MILLESIMO"), "0")
                    RIGA1.Item("IMPORTO") = (ImpDaDividere / 1000) * par.IfNull(myReader5("VALORE_MILLESIMO"), 0)
                    DTUnita.Rows.Add(RIGA1)

                Loop
                myReader5.Close()
            Next


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()





        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function
End Class
