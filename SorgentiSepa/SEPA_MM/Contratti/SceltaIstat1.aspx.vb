
Partial Class Contratti_SceltaIstat1
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Public Property sStringaSql() As String
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


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            Try

                par.OracleConn.Open()
                par.SettaCommand(par)

                Dim PERIODO As String = Request.QueryString("P")
                sStringaSql = PERIODO

                Dim Str As String = ""

                Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
                Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='Caricamento in corso' ><br>Caricamento in corso..."
                Str = Str & "<" & "/div>"

                Response.Write(Str)
                Response.Flush()


                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ADEGUAMENTO_ISTAT WHERE ANNO_MESE='" & sStringaSql & "'"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.HasRows = True Then
                    Response.Write("<script>alert('Attenzione, Aggiornamento ISTAT già effettuato per questo periodo. Continuando, il precedente aggiornamento sarà annullato per essere poi rigenerato.');</script>")
                End If
                myReader.Close()

                Str = "SELECT COD_CONTRATTO,to_char(to_date(data_DECORRENZA,'yyyymmdd'),'dd/mm/yyyy') as ""DECORRENZA"",(select SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ""IMP_CANONE_ATTUALE"" ,PERC_ISTAT  FROM SISCOM_MI.RAPPORTI_UTENZA WHERE PERC_ISTAT>0 AND (SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID)='IN CORSO' OR SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID)='IN CORSO (S.T.)') and substr(data_decorrenza,1,4)<'" & Mid(PERIODO, 1, 4) & "' and substr(data_decorrenza,5,2)='" & Mid(PERIODO, 5, 2) & "'"
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Str, par.OracleConn)

                Dim ds As New Data.DataTable

                da.Fill(ds)

                DataGridVarIstat.DataSource = ds
                DataGridVarIstat.DataBind()

                Label1.Text = "Elenco contratti da aggiornare a " & par.ConvertiMese(Mid(PERIODO, 5, 2)) & " " & Mid(PERIODO, 1, 4) & " (Num. " & DataGridVarIstat.Items.Count & ")"



                par.OracleConn.Close()

                HttpContext.Current.Session.Add("AA", ds)
                imgExport.Attributes.Add("onclick", "javascript:window.open('Report/DownLoad.aspx?CHIAMA=99','Istat','');")

            Catch ex As Exception
                par.OracleConn.Close()
                Session.Add("ERRORE", "Provenienza:Simulazione Bollette - " & ex.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            End Try

        End If

    End Sub

    Protected Sub ImgIndietro_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgIndietro.Click
        Response.Redirect("SceltaIstat.aspx")
    End Sub

    Protected Sub imgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgProcedi.Click
        Try
            Dim Str As String = ""
            Dim CANONE_AGGIORNATO As Double
            Dim INDICE_ATTUALE As Double = 0
            Dim INDICE_PRECEDENTE As Double = 0
            Dim VAR_75 As Double = 0
            Dim VAR_100 As Double = 0
            Dim INDICE As Double = 0
            Dim AUMENTO As Double = 0
            Dim I As Integer = 0
            Dim CANONE_CORRENTE As Double = 0


            Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
            Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='Elaborazione in corso' ><br>Elaborazione in corso..."
            Str = Str & "<" & "/div>"

            Response.Write(Str)
            Response.Flush()

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.VARIAZIONI_ISTAT ORDER BY ID DESC"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                INDICE_ATTUALE = myReader1("INDICE_NAZIONALE")
                VAR_75 = myReader1("VAR_75_ANNUALE")
                VAR_100 = myReader1("VAR_100_ANNUALE")
            End If
            myReader1.Close()


            'ANNULLO ADEGUAMENTO E RIPRISTINO PRECEDENTE
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ADEGUAMENTO_ISTAT WHERE ANNO_MESE='" & sStringaSql & "'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader.Read
                'par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA SET IMP_CANONE_ATTUALE=" & par.VirgoleInPunti(par.IfNull(myReader("IMPORTO_CANONE_INIZIALE"), 0)) & " WHERE ID=" & myReader("ID_CONTRATTO")
                'par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                    & "VALUES (" & myReader("ID_CONTRATTO") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F10','Annullato ISTAT " & par.ConvertiMese(Mid(sStringaSql, 5, 2)) & " " & Mid(sStringaSql, 1, 4) & "')"
                par.cmd.ExecuteNonQuery()

            Loop
            MYREADER.CLOSE()

            par.cmd.CommandText = "delete from siscom_mi.adeguamento_istat where anno_mese='" & sStringaSql & "'"
            par.cmd.ExecuteNonQuery()
            '-----------------------

            par.cmd.CommandText = "SELECT ID,COD_CONTRATTO,to_char(to_date(data_DECORRENZA,'yyyymmdd'),'dd/mm/yyyy') as ""DECORRENZA"",IMP_CANONE_INIZIALE ,PERC_ISTAT,(select SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ""ADEGUAMENTI""  FROM SISCOM_MI.RAPPORTI_UTENZA WHERE PERC_ISTAT>0 AND (SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID)='IN CORSO' OR SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID)='IN CORSO (S.T.)') and substr(data_decorrenza,1,4)<'" & Mid(sStringaSql, 1, 4) & "' and substr(data_decorrenza,5,2)='" & Mid(sStringaSql, 5, 2) & "'"
            myReader = par.cmd.ExecuteReader()
            Do While myReader.Read
                I = I + 1
                If myReader("PERC_ISTAT") = 75 Then
                    INDICE = VAR_75
                End If

                If myReader("PERC_ISTAT") = 100 Then
                    INDICE = VAR_100
                End If

                INDICE_PRECEDENTE = INDICE


                CANONE_CORRENTE = par.IfNull(myReader("IMP_CANONE_ATTUALE"), 0) + par.IfNull(myReader("ADEGUAMENTI"), 0)

                'par.cmd.CommandText = "SELECT SUM(IMPORTO_TR_AGG) FROM SISCOM_MI.ADEGUAMENTO_ISTAT WHERE ID_CONTRATTO=" & myReader("ID")
                'myReader1 = par.cmd.ExecuteReader()
                'If myReader1.Read Then
                '    CANONE_CORRENTE = CANONE_CORRENTE + par.IfNull(myReader1(0), 0)
                'End If
                'myReader1.Close()


                AUMENTO = Format(((par.IfNull(CANONE_CORRENTE, 0) * INDICE) / 100), "0.00")
                CANONE_AGGIORNATO = Format(par.IfNull(CANONE_CORRENTE, 0) + AUMENTO, "0.00")



                par.cmd.CommandText = "Insert into SISCOM_MI.ADEGUAMENTO_ISTAT (ID, ID_CONTRATTO, DATA_AGG_INIZIO, DATA_AGG_FINE, " _
                                    & "IMPORTO_CANONE_AGG, COD_TR, IMPORTO_TR_AGG, NRO_PROT_LETTERA, DATA_LETTERA, " _
                                    & "IMPORTO_CANONE_INIZIALE, INDICE_INIZIALE, INDICE_FINALE, VAR_ISTAT, BASE_INIZIALE, BASE_FINALE, " _
                                    & "COEF_RACCORDO, ANNO_MESE) Values " _
                                    & "(SISCOM_MI.SEQ_ADEGUAMENTO_ISTAT.NEXTVAL, " & myReader("ID") & ", '" & sStringaSql & "01' , '" _
                                    & Year(Now) + 1 & Mid(sStringaSql, 5, 2) & "01', " & par.VirgoleInPunti(CANONE_AGGIORNATO) & " , " _
                                    & "'', " & par.VirgoleInPunti(AUMENTO) & ", '', '', " & par.VirgoleInPunti(par.IfNull(CANONE_CORRENTE, 0)) & " , " _
                                    & par.VirgoleInPunti(INDICE_PRECEDENTE) & ", " & par.VirgoleInPunti(INDICE_ATTUALE) _
                                    & ", " & par.VirgoleInPunti(par.IfNull(myReader("PERC_ISTAT"), "0")) & ", NULL, NULL, " _
                                    & "NULL, '" & sStringaSql & "')"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE (ID_CONTRATTO,DATA_APPLICAZIONE,ID_MOTIVO,IMPORTO,NOTE) VALUES (" & myReader("ID") & ",'" & Format(Now, "yyyyMMdd") & "'," & par.VirgoleInPunti(AUMENTO) & ",'AGG.ISTAT DAL " & par.FormattaData(sStringaSql & "01") & " AL " & par.FormattaData(Year(Now) + 1 & Mid(sStringaSql, 5, 2) & "01") & "')"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                    & "VALUES (" & myReader("ID") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                    & "'F09','Aggiornato ISTAT " & par.ConvertiMese(Mid(sStringaSql, 5, 2)) & " " & Mid(sStringaSql, 1, 4) & "')"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA SET IMP_CANONE_ATTUALE=" & par.VirgoleInPunti(CANONE_AGGIORNATO) & " WHERE ID=" & myReader("ID")
                par.cmd.ExecuteNonQuery()

            Loop
            myReader.Close()

            If I > 0 Then
                Str = "SELECT to_char(to_date(ADEGUAMENTO_ISTAT.DATA_AGG_INIZIO,'yyyymmdd'),'dd/mm/yyyy') as ""INIZIO"",to_char(to_date(ADEGUAMENTO_ISTAT.DATA_AGG_FINE,'yyyymmdd'),'dd/mm/yyyy') as ""FINE"",RAPPORTI_UTENZA.ID,COD_CONTRATTO,to_char(to_date(data_DECORRENZA,'yyyymmdd'),'dd/mm/yyyy') as ""DECORRENZA"",PERC_ISTAT,ADEGUAMENTO_ISTAT.IMPORTO_CANONE_INIZIALE AS ""IMP_CANONE_INIZIALE"" ,IMPORTO_TR_AGG,IMPORTO_CANONE_AGG  FROM SISCOM_MI.ADEGUAMENTO_ISTAT,SISCOM_MI.RAPPORTI_UTENZA WHERE ADEGUAMENTO_ISTAT.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND ADEGUAMENTO_ISTAT.ANNO_MESE='" & sStringaSql & "' "
                HttpContext.Current.Session.Add("BB", Str)
                Response.Write("<script>window.open('SceltaIstat2.aspx');</script>")
                Response.Write("<script>location.href='pagina_home.aspx';</script>")
            End If


            par.myTrans.Commit()
            par.OracleConn.Close()

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:Simulazione Bollette - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
End Class
