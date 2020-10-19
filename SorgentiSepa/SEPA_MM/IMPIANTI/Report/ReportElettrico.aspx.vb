
Partial Class ASS_ReportElettrico
    Inherits PageSetIdMode

    Dim par As New CM.Global

    Dim vIdImpianto As String
    Dim dt As New Data.DataTable




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StringaSql As String
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader


        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then

            vIdImpianto = Request.QueryString("ID_IMPIANTO")

            If IsNumeric(vIdImpianto) Then

                Try
                    ' LEGGO IMPIANTO ELETTRICO
                    Label2.Text = "IMPIANTO CENTRALE ELETTRICA"

                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    StringaSql = " select COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS ""COMPLESSO""," _
                                      & " EDIFICI.DENOMINAZIONE AS ""EDIFICIO"",IMPIANTI.* " _
                               & " from SISCOM_MI.IMPIANTI, SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.EDIFICI " _
                               & " where IMPIANTI.ID =" & vIdImpianto _
                               & " and   IMPIANTI.ID_COMPLESSO=COMPLESSI_IMMOBILIARI.ID (+)" _
                               & " and   IMPIANTI.ID_EDIFICIO=EDIFICI.ID (+) "

                    par.cmd.CommandText = StringaSql
                    myReader1 = par.cmd.ExecuteReader()

                    If myReader1.Read Then

                        lblCodice.Text = par.IfNull(myReader1("COD_IMPIANTO"), "")

                        lblComplesso.Text = par.IfNull(myReader1("COMPLESSO"), "")
                        lblEdificio.Text = par.IfNull(myReader1("EDIFICIO"), "")


                        lblDenominazione.Text = par.IfNull(myReader1("DESCRIZIONE"), "")
                        lblDitta.Text = par.IfNull(myReader1("DITTA_COSTRUTTRICE"), "")
                        lblData.Text = par.FormattaData(par.IfNull(myReader1("ANNO_COSTRUZIONE"), ""))


                        Me.lblNote.Text = par.IfNull(myReader1("ANNOTAZIONI"), "")

                        StringaSql = "select * from SISCOM_MI.I_ELETTRICI where SISCOM_MI.I_ELETTRICI.ID = " & vIdImpianto

                        par.cmd.CommandText = StringaSql
                        myReader2 = par.cmd.ExecuteReader()

                        If myReader2.Read Then

                            Me.lblAvanquadro.Text = par.IfNull(myReader2("AVANQUADRO"), "")
                            Me.lblNorma.Text = par.IfNull(myReader2("NORMA"), "")
                            Me.lblDifferenziale.Text = par.IfNull(myReader2("DIFFERENZIALE"), "")

                            Me.lblDittaGestione.Text = par.IfNull(myReader2("DITTA_GESTIONE"), "")
                            Me.lblTel.Text = par.IfNull(myReader2("TELEFONO_DITTA"), "")

                        End If
                        myReader2.Close()

                    End If
                    myReader1.Close()


                    '*** SERVIZI
                    StringaSql = "select SISCOM_MI.I_ELE_QUADRO_SERVIZI.ID,SISCOM_MI.I_ELE_QUADRO_SERVIZI.QUANTITA,SISCOM_MI.I_ELE_QUADRO_SERVIZI.DIFFERENZIALE,SISCOM_MI.I_ELE_QUADRO_SERVIZI.NORMA,SISCOM_MI.I_ELE_QUADRO_SERVIZI.UBICAZIONE," _
                              & " (select count(*) from SISCOM_MI.I_ELE_QUADRO_SE_ELEMENTI where  SISCOM_MI.I_ELE_QUADRO_SE_ELEMENTI.ID_QUADRO_SERVIZI=SISCOM_MI.I_ELE_QUADRO_SERVIZI.ID) AS ""SCALE_SERVITE"" " _
                              & " from SISCOM_MI.I_ELE_QUADRO_SERVIZI " _
                              & " where SISCOM_MI.I_ELE_QUADRO_SERVIZI.ID_IMPIANTO = " & vIdImpianto _
                              & " order by SISCOM_MI.I_ELE_QUADRO_SERVIZI.ID "


                    par.cmd.CommandText = StringaSql

                    Dim daSE As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dsSE As New Data.DataSet()

                    daSE.Fill(dsSE, "I_ELE_QUADRO_SERVIZI")

                    DataGridServizio.DataSource = dsSE
                    DataGridServizio.DataBind()

                    dsSE.Dispose()
                    '****************************+


                    '*** SCALA
                    StringaSql = "select SISCOM_MI.I_ELE_QUADRO_SCALA.ID,SISCOM_MI.I_ELE_QUADRO_SCALA.QUANTITA,SISCOM_MI.I_ELE_QUADRO_SCALA.DIFFERENZIALE,SISCOM_MI.I_ELE_QUADRO_SCALA.NORMA,SISCOM_MI.I_ELE_QUADRO_SCALA.UBICAZIONE," _
                                    & " (select count(*) from SISCOM_MI.I_ELE_QUADRO_SC_ELEMENTI where  SISCOM_MI.I_ELE_QUADRO_SC_ELEMENTI.ID_QUADRO_SCALA=SISCOM_MI.I_ELE_QUADRO_SCALA.ID) AS ""SCALE_SERVITE"" " _
                              & " from SISCOM_MI.I_ELE_QUADRO_SCALA " _
                              & " where SISCOM_MI.I_ELE_QUADRO_SCALA.ID_IMPIANTO = " & vIdImpianto _
                              & " order by SISCOM_MI.I_ELE_QUADRO_SCALA.ID "


                    par.cmd.CommandText = StringaSql

                    Dim daSC As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dsSC As New Data.DataSet()

                    daSC.Fill(dsSC, "I_ELE_QUADRO_SCALA")

                    DataGridScala.DataSource = dsSC
                    DataGridScala.DataBind()

                    dsSC.Dispose()
                    '*************************


                    '*** BOX
                    StringaSql = "select SISCOM_MI.I_ELE_BOX.ID,SUP_9_AUTO,QUADRO,DIFFERENZIALE," _
                                   & "SISCOM_MI.TIPO_DITRIBUZIONE_I_ELE.DESCRIZIONE AS ""DISTRIBUZIONE""," _
                                   & "ID_TIPO_DISTRIBUZIONE,PULSANTE_SGANCIO,PRATICA_VVF,VERIFICA," _
                                   & "MESSA_TERRA, SCARICHE_ATMOSFERICHE,SCARICATORI,NOTE " _
                          & " from SISCOM_MI.I_ELE_BOX,SISCOM_MI.TIPO_DITRIBUZIONE_I_ELE " _
                          & " where SISCOM_MI.I_ELE_BOX.ID_IMPIANTO = " & vIdImpianto _
                          & " and   SISCOM_MI.I_ELE_BOX.ID_TIPO_DISTRIBUZIONE=SISCOM_MI.TIPO_DITRIBUZIONE_I_ELE.ID (+) " _
                          & " order by SISCOM_MI.I_ELE_BOX.ID "

                    par.cmd.CommandText = StringaSql

                    Dim daBox As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dsBox As New Data.DataSet()

                    daBox.Fill(dsBox, "I_ELE_BOX")


                    DataGridBox.DataSource = dsBox
                    DataGridBox.DataBind()

                    dsBox.Dispose()
                    '**********************************

                    '*** PORTINERIA
                    StringaSql = "select SISCOM_MI.I_ELE_PORTINERIA.ID,QUADRO," _
                                & "DIFFERENZIALE,NORMA," _
                                   & "SISCOM_MI.TIPO_DITRIBUZIONE_I_ELE.DESCRIZIONE AS ""DISTRIBUZIONE""," _
                                   & "ID_TIPO_DISTRIBUZIONE,NOTE " _
                          & " from SISCOM_MI.I_ELE_PORTINERIA,SISCOM_MI.TIPO_DITRIBUZIONE_I_ELE " _
                          & " where SISCOM_MI.I_ELE_PORTINERIA.ID_IMPIANTO = " & vIdImpianto _
                          & " and   SISCOM_MI.I_ELE_PORTINERIA.ID_TIPO_DISTRIBUZIONE=SISCOM_MI.TIPO_DITRIBUZIONE_I_ELE.ID (+) " _
                          & " order by SISCOM_MI.I_ELE_PORTINERIA.ID "

                    par.cmd.CommandText = StringaSql

                    Dim daPortineria As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dsPortineria As New Data.DataSet()

                    daPortineria.Fill(dsPortineria, "I_ELE_PORTINERIA")


                    DataGridPortineria.DataSource = dsPortineria
                    DataGridPortineria.DataBind()

                    dsPortineria.Dispose()
                    '**********************************


                    '*** EVENTI_IMPIANTI
                    par.InserisciEvento(par.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.STAMPA_IMPIANTO, "")


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


End Class
