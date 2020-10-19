Imports System.Collections.Generic

Partial Class Contratti_Interessi
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String = ""

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='Caricamento in corso' ><br>Caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()

        If Not IsPostBack Then
            Try

                par.OracleConn.Open()
                par.SettaCommand(par)

                cmbComplesso.Items.Clear()
                cmbEdificio.Items.Clear()
                cmbUnita.Items.Clear()


                cmbComplesso.Items.Add(New ListItem("TUTTI", -1))
                cmbEdificio.Items.Add(New ListItem("TUTTI", -1))
                cmbUnita.Items.Add(New ListItem("TUTTI", -1))

                par.cmd.CommandText = "SELECT id,denominazione FROM SISCOM_MI.complessi_immobiliari order by denominazione asc"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read

                    cmbComplesso.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
                End While
                myReader1.Close()

                cmbComplesso.SelectedIndex = -1
                cmbComplesso.Items.FindByValue("-1").Selected = True


                par.OracleConn.Close()
            Catch ex As Exception
                par.OracleConn.Close()
                Session.Add("ERRORE", "Provenienza:Simulazione Bollette - " & ex.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            End Try
        End If
        txtData.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub

    Protected Sub cmbComplesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComplesso.SelectedIndexChanged
        Try


            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            cmbEdificio.Items.Clear()
            cmbUnita.Items.Clear()

            cmbEdificio.Items.Add(New ListItem("TUTTI", -1))
            cmbUnita.Items.Add(New ListItem("TUTTI", -1))

            par.cmd.CommandText = "SELECT distinct(id),denominazione FROM SISCOM_MI.edifici where id_complesso=" & cmbComplesso.SelectedValue & " order by denominazione asc"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
            End While
            myReader1.Close()
            par.OracleConn.Close()
        Catch ex As Exception
            par.OracleConn.Close()
        End Try
    End Sub

    Protected Sub cmbEdificio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEdificio.SelectedIndexChanged
        Try
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            cmbUnita.Items.Clear()
            cmbUnita.Items.Add(New ListItem("TUTTI", -1))

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.unita_immobiliari where id_edificio=" & cmbEdificio.SelectedValue & " order by scala asc"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                cmbUnita.Items.Add(New ListItem(par.IfNull(myReader1("COD_UNITA_IMMOBILIARE"), " "), par.IfNull(myReader1("ID"), "-1")))
            End While
            myReader1.Close()
            par.OracleConn.Close()

        Catch ex As Exception
            par.OracleConn.Close()
        End Try
    End Sub

    Protected Sub imgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgProcedi.Click
        Try
            Dim sAggiunta As String = ""
            Dim DataCalcolo As String = ""
            Dim DataInizio As String = ""
            Dim I As Integer = 0
            Dim tasso As Double = 0
            Dim baseCalcolo As Double = 0

            Dim Giorni As Integer = 0
            Dim GiorniAnno As Integer = 0
            Dim dataPartenza As String = ""

            Dim Totale As Double = 0
            Dim TotalePeriodo As Double = 0
            Dim indice As Long = 0
            Dim DataFine As String = ""

            If par.IfEmpty(txtData.Text, "") = "" Then
                Response.Write("<script>alert('Inserire una data valida!');</script>")
                Exit Sub
            End If

            Dim Interessi As New SortedDictionary(Of Integer, Double)


            Dim Str1 As String = ""

            Str1 = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
            Str1 = Str1 & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='Elaborazione in corso' ><br>Elaborazione in corso...<br />Questa operazione potrebbe richiedere alcuni minuti!"
            Str1 = Str1 & "<" & "/div>"

            Response.Write(Str1)
            Response.Flush()

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans


            par.cmd.CommandText = "select * from siscom_mi.tab_interessi_legaLI order by anno desc"

            Interessi.Clear()
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReaderA.Read
                Interessi.Add(myReaderA("anno"), myReaderA("tasso"))
            Loop
            myReaderA.Close()

            DataCalcolo = par.AggiustaData(txtData.Text)

            If cmbComplesso.SelectedItem.Value <> "-1" Then
                sAggiunta = "EDIFICI.ID_COMPLESSO=" & cmbComplesso.SelectedItem.Value
            End If

            If cmbEdificio.SelectedItem.Value <> "-1" Then
                If sAggiunta <> "" Then sAggiunta = sAggiunta & " AND "
                sAggiunta = "UNITA_CONTRATTUALE.ID_EDIFICIO=" & cmbEdificio.SelectedItem.Value
            End If

            If cmbUnita.SelectedItem.Value <> "-1" Then
                If sAggiunta <> "" Then sAggiunta = sAggiunta & " AND "
                sAggiunta = "UNITA_CONTRATTUALE.COD_UNITA_IMMOBILIARE='" & cmbUnita.SelectedItem.Text & "' "
            End If
            If sAggiunta <> "" Then sAggiunta = sAggiunta & " AND "




            par.cmd.CommandText = "select RAPPORTI_UTENZA.*,siscom_mi.getstatocontratto(RAPPORTI_UTENZA.id) as ""STATO_CONTRATTO"",EDIFICI.ID_COMPLESSO,UNITA_CONTRATTUALE.ID_EDIFICIO," _
                                           & "UNITA_CONTRATTUALE.ID_UNITA FROM SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.EDIFICI," _
                                           & "SISCOM_MI.RAPPORTI_UTENZA WHERE " & sAggiunta & " BOZZA='0' AND INTERESSI_CAUZIONE='1' " _
                                           & "AND " _
                                           & "UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND " _
                                           & "UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND EDIFICI.ID=UNITA_CONTRATTUALE.ID_EDIFICIO"

            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader.Read

                par.cmd.CommandText = "delete from siscom_mi.adeguamento_interessi where fl_applicato=0 AND ID_CONTRATTO=" & myReader("id")
                par.cmd.ExecuteNonQuery()

                baseCalcolo = par.IfNull(myReader("imp_deposito_cauz"), 0)
                If baseCalcolo > 0 Then

                    If par.IfNull(myReader("data_riconsegna"), "29991231") < DataCalcolo Then
                        DataCalcolo = myReader("data_riconsegna")
                    End If

                    par.cmd.CommandText = "select * from siscom_mi.adeguamento_interessi where id_contratto=" & myReader("id") & " and fl_applicato=1 order by id desc"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.HasRows = False Then
                        DataInizio = myReader("data_decorrenza")
                    End If
                    If myReaderA.Read Then
                        DataInizio = Format(DateAdd(DateInterval.Day, 1, CDate(par.FormattaData(myReaderA("data")))), "yyyyMMdd")
                    End If
                    myReaderA.Close()

                    If DataInizio < "20091001" Then
                        DataInizio = "20091001"
                    End If

                    Giorni = 0
                    GiorniAnno = 0
                    dataPartenza = DataInizio
                    Totale = 0
                    TotalePeriodo = 0

                    par.cmd.CommandText = "insert into siscom_mi.adeguamento_interessi (id,id_contratto,data,fl_applicato) values (siscom_mi.seq_adeguamento_interessi.nextval," & myReader("id") & ",'" & DataCalcolo & "',0)"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "select siscom_mi.seq_adeguamento_interessi.currval from dual"
                    myReaderA = par.cmd.ExecuteReader()
                    indice = 0
                    If myReaderA.Read Then
                        indice = myReaderA(0)
                    End If

                    For I = CInt(Mid(DataInizio, 1, 4)) To CInt(Mid(DataCalcolo, 1, 4))

                        If I = CInt(Mid(DataCalcolo, 1, 4)) Then
                            DataFine = par.FormattaData(DataCalcolo)
                        Else
                            DataFine = "31/12/" & I

                        End If

                        GiorniAnno = DateDiff(DateInterval.Day, CDate("01/01/" & I), CDate("31/12/" & I)) + 1

                        Giorni = DateDiff(DateInterval.Day, CDate(par.FormattaData(dataPartenza)), CDate(DataFine)) + 1

                        If I < 1990 Then
                            tasso = 5
                        Else
                            If Interessi.ContainsKey(I) = True Then
                                tasso = Interessi(I)
                            End If
                        End If

                        TotalePeriodo = Format((((baseCalcolo * tasso) / 100) / GiorniAnno) * Giorni, "0.00")
                        Totale = Totale + TotalePeriodo



                        par.cmd.CommandText = "insert into siscom_mi.adeguamento_interessi_voci (id_adeguamento,dal,al,giorni,tasso,importo) values (" & indice & ",'" & dataPartenza & "','" & Format(CDate(DataFine), "yyyyMMdd") & "'," & Giorni & "," & par.VirgoleInPunti(tasso) & "," & par.VirgoleInPunti(TotalePeriodo) & ")"
                        par.cmd.ExecuteNonQuery()

                        dataPartenza = I + 1 & "0101"

                    Next
                    par.cmd.CommandText = "update siscom_mi.adeguamento_interessi set importo=" & par.VirgoleInPunti(Format(Totale, "0.00")) & " where id=" & indice
                    par.cmd.ExecuteNonQuery()
                End If
            Loop
            myReader.Close()

            par.myTrans.Commit()
            par.OracleConn.Close()

            Dim Str As String = "select rapporti_utenza.cod_contratto,ADEGUAMENTO_INTERESSI.DATA,ADEGUAMENTO_INTERESSI.IMPORTO AS ""TOTALE"",ADEGUAMENTO_INTERESSI_voci.* from SISCOM_MI.rapporti_utenza,SISCOM_MI.ADEGUAMENTO_INTERESSI,SISCOM_MI.ADEGUAMENTO_INTERESSI_voci where rapporti_utenza.id=adeguamento_interessi.id_contratto and ADEGUAMENTO_INTERESSI_voci.id_adeguamento=ADEGUAMENTO_INTERESSI.id and ADEGUAMENTO_INTERESSI.FL_APPLICATO=0 AND adeguamento_interessi.id_contratto in (select RAPPORTI_UTENZA.ID FROM SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.EDIFICI,SISCOM_MI.RAPPORTI_UTENZA WHERE " & sAggiunta & " BOZZA='0' AND INTERESSI_CAUZIONE='1' And UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND EDIFICI.ID=UNITA_CONTRATTUALE.ID_EDIFICIO) order by id_contratto,dal asc"

            HttpContext.Current.Session.Add("BB", Str)
            Response.Write("<script>window.open('VisInteressi.aspx');</script>")
            Response.Write("<script>location.href='pagina_home.aspx';</script>")











        Catch ex As Exception

            par.myTrans.Rollback()
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:Simulazione Bollette - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub
End Class
