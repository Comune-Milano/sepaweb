Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Imports ICSharpCode.SharpZipLib.Zip

Partial Class Contratti_CONTRATTI_LIGHT_ElencoAllegatiExGestori
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE_AU_LIGHT") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim MiaSHTML As String
        Try

            par.OracleConn.Open()
            par.SettaCommand(par)



            Dim MIOCOLORE As String
            Dim i As Integer
            Dim ElencoFile()

            Dim j As Integer
            Dim Conduttore As String = ""
            Dim IDCONTRATTO As String = ""

            Label1.Text = "Contratto Codice " & Request.QueryString("cod")

            par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.ID AS IDC,anagrafica.id,CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS ""INTESTATARIO"" FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI,siscom_mi.rapporti_utenza WHERE rapporti_utenza.cod_CONTRATTO='" & Request.QueryString("cod") & "' and SOGGETTI_CONTRATTUALI.id_contratto=rapporti_utenza.id AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader1.Read
                Conduttore = Conduttore & par.IfNull(myReader1("INTESTATARIO"), "") & ", "
                IDCONTRATTO = Format(myReader1("IDC"), "000000000000000")
            Loop
            myReader1.Close()
            Label1.Text = Label1.Text & "<br/>Intestatario/i:" & Conduttore

            par.cmd.CommandText = "select unita_immobiliari.cod_tipologia,complessi_immobiliari.id as idq,COMPLESSI_IMMOBILIARI.ID_QUARTIERE,edifici.id as idf,edifici.fl_piano_vendita,EDIFICI.GEST_RISC_DIR,edifici.condominio,tipo_livello_piano.descrizione as miopiano,(select descrizione from siscom_mi.scale_edifici where id=unita_immobiliari.id_scala) as SCALA,indirizzi.cap,comuni_nazioni.nome as comune,unita_immobiliari.id_destinazione_uso,SISCOM_MI.UNITA_IMMOBILIARI.interno,UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE,indirizzi.descrizione,indirizzi.civico,indirizzi.cap from siscom_mi.tipo_livello_piano,comuni_nazioni,siscom_mi.indirizzi,SISCOM_MI.UNITA_IMMOBILIARI,siscom_mi.edifici,SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO AND edifici.id=unita_immobiliari.id_edificio and unita_immobiliari.cod_tipo_livello_piano=tipo_livello_piano.cod (+) and indirizzi.cod_comune=comuni_nazioni.cod (+) and unita_immobiliari.id_indirizzo=indirizzi.id (+) and unita_immobiliari.cod_unita_immobiliare='" & Mid(Request.QueryString("cod"), 1, 17) & "'"
            Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader3.Read Then
                Label1.Text = Label1.Text & "<br/>Indirizzo: " & par.IfNull(myReader3("descrizione"), "") & ", " & par.IfNull(myReader3("civico"), "") _
                & "</br>" & par.IfNull(myReader3("cap"), "") & " " & par.IfNull(myReader3("comune"), "") & "</br>Interno: " & par.IfNull(myReader3("interno"), "") & " Scala: " & par.IfNull(myReader3("scala"), "") & " Piano: " & par.IfNull(myReader3("miopiano"), "")
            End If
            myReader3.Close()

            MiaSHTML = "<table border='0' cellpadding='1' cellspacing='1' width='100%'>" & vbCrLf
            MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='40%'><font size='2' face='Arial'>Tipo Allegato</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='40%'><font size='2' face='Arial'>Descrizione</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='20%'><font size='2' face='Arial'>Download</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "</tr>" & vbCrLf

            i = 0
            MIOCOLORE = "#CCFFFF"

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.DOCUMENTI_EX_GESTORI WHERE ID_CONTRATTO=" & IDCONTRATTO & " ORDER BY DESCRIZIONE ASC"
            myReader1 = par.cmd.ExecuteReader()
            Do While myReader1.Read
                MiaSHTML = MiaSHTML & "<tr>" & vbCrLf

                Select Case UCase(myReader1("TIPO_DOC"))
                    Case "001"
                        MiaSHTML = MiaSHTML & "<td width='40%' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>CONGUAGLIO</font></td>" & vbCrLf
                    Case "002"
                        MiaSHTML = MiaSHTML & "<td width='40%' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>SOLLECITO</font></td>" & vbCrLf
                    Case "003"
                        MiaSHTML = MiaSHTML & "<td width='40%' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>RICEVUTA</font></td>" & vbCrLf
                    Case Else
                        MiaSHTML = MiaSHTML & "<td width='40%' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>NON DEFINITO</font></td>" & vbCrLf
                End Select
                MiaSHTML = MiaSHTML & "<td width='40%' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & UCase(par.IfNull(myReader1("descrizione"), "NON DEFINITO")) & "</font></td>" & vbCrLf
                MiaSHTML = MiaSHTML & "<td width='20%' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a  alt='Download' href='../../ALLEGATI/CONTRATTI/EX_GESTORE/" & Mid(myReader1("NOME_FILE_ORI"), 1, 4) & "/" & myReader1("NOME_FILE_ORI") & "' target='_blank'><img src='../../ImmMaschere/MenuTopDownload.gif' border='0'></a></font></td>" & vbCrLf
                MiaSHTML = MiaSHTML & "<td width='100px' bgcolor='" & MIOCOLORE & "'>&nbsp;</td>" & vbCrLf
                MiaSHTML = MiaSHTML & "</tr>" & vbCrLf
                If MIOCOLORE = "#CCFFFF" Then
                    MIOCOLORE = "#FFFFCC"
                Else
                    MIOCOLORE = "#CCFFFF"
                End If
            Loop
            myReader1.Close()
            MiaSHTML = MiaSHTML & "</table>" & vbCrLf

            Label3.Text = MiaSHTML

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex1 As Oracle.DataAccess.Client.OracleException
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            If ex1.Number = 942 Then
                MiaSHTML = MiaSHTML & "</table>" & vbCrLf
                Label3.Text = MiaSHTML
            Else
                Label3.Text = ex1.Message
            End If
        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Label3.Text = ex.Message

        End Try
    End Sub

    Private Function RicavaFile(ByVal sFile) As String
        Dim N

        For N = Len(sFile) To 1 Step -1
            If Mid(sFile, N, 1) = "\" Then
                Exit For
            End If
        Next

        RicavaFile = Right(sFile, Len(sFile) - N)

    End Function
End Class
