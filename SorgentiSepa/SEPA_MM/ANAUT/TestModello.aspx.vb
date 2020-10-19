Imports System.IO
Imports SubSystems.RP

Partial Class ANAUT_TestModello
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim BaseFile As String = "MODELLO_" & Format(Now, "yyyyMMddHHmmss")
            Dim file1 As String = BaseFile & ".RTF"
            Dim fileName As String = Server.MapPath("..\FileTemp\") & file1
            Dim fileNamePDF As String = Server.MapPath("..\FileTemp\") & BaseFile & ".pdf"
            Dim NomeModello As String = ""

            Dim trovato As Boolean = False

            par.cmd.CommandText = "SELECT * FROM UTENZA_TIPO_DOC WHERE id=" & Request.QueryString("ID")
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                NomeModello = myReader("descrizione")
                Dim bw As BinaryWriter
                If par.IfNull(myReader("MODELLO"), "").LENGTH > 0 Then
                    Dim fs As New FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write)
                    bw = New BinaryWriter(fs)
                    bw.Write(myReader("MODELLO"))
                    bw.Flush()
                    bw.Close()
                    trovato = True
                End If
            End If
            myReader.Close()

            If trovato = True Then
                Dim sr1 As StreamReader = New StreamReader(fileName, System.Text.Encoding.GetEncoding("iso-8859-1"))
                Dim contenuto As String = sr1.ReadToEnd()
                sr1.Close()

                contenuto = Replace(contenuto, "$testoresponsabile$", "Il Responsabile")
                contenuto = Replace(contenuto, "$data$", "XXXXXXXX")
                contenuto = Replace(contenuto, "$datastampa$", "XXXXXXXX")
                contenuto = Replace(contenuto, "$dataappuntamento$", "XXXXXXXX")
                contenuto = Replace(contenuto, "$oreappuntamento$", "XXXXXXXX")
                contenuto = Replace(contenuto, "$annoau$", "XXXX")
                contenuto = Replace(contenuto, "$annoredditi$", "XXXX")
                contenuto = Replace(contenuto, "$documentimancanti$", "XXXXXXXX")
                contenuto = Replace(contenuto, "$dichiarante$", "XXXXXXXX XXXXXXXX")
                contenuto = Replace(contenuto, "$datanascitadichiarante$", "XXXXXXXXXX")
                contenuto = Replace(contenuto, "$luogonascitadichiarante$", "XXXXXXXX")
                contenuto = Replace(contenuto, "$provincianascitadichiarante$", "XX")

                contenuto = Replace(contenuto, "$codcontratto$", "XXXXXXXXXXXXXXXXXXX")
                contenuto = Replace(contenuto, "$intestatario$", "XXXXXXXX XXXXXXXX")
                contenuto = Replace(contenuto, "$nominativocorr$", "XXXXXXXX XXXXXXXX")
                contenuto = Replace(contenuto, "$indirizzocorr$", "XXXXXXXXX")
                contenuto = Replace(contenuto, "$localitacorr$", "XXXXXXXXX")

                contenuto = Replace(contenuto, "$internoscalapiano$", "XXXXXXXXX")
                contenuto = Replace(contenuto, "$internoscalapianocorr$", "XXXXXXXXX")
                contenuto = Replace(contenuto, "$interno$", "XXXX")
                contenuto = Replace(contenuto, "$scala$", "XXXX")
                contenuto = Replace(contenuto, "$piano$", "XXXX")
                contenuto = Replace(contenuto, "$indirizzounita$", "XXXXXXXX, XX")
                contenuto = Replace(contenuto, "$localitaunita$", "XXXXX XXXXXXXXXX XX")
                contenuto = Replace(contenuto, "$nomefiliale$", "XXXXXXXXX")
                contenuto = Replace(contenuto, "$indirizzofiliale$", "XXXXXXXXX, XX")
                contenuto = Replace(contenuto, "$capfiliale$", "XXXXX")
                contenuto = Replace(contenuto, "$cittafiliale$", "XXXXXXXXX")
                contenuto = Replace(contenuto, "$telfax$", "Tel:" & "XXXXXXXXX" & " - Fax:" & "XXXXXXXXX")
                contenuto = Replace(contenuto, "$referente$", "XXXXXXXXX")
                contenuto = Replace(contenuto, "$operatore$", Session.Item("NOME_OPERATORE"))
                contenuto = Replace(contenuto, "$responsabile$", "XXXXXXXXX")
                contenuto = Replace(contenuto, "$numverdefiliale$", "XXXXXXXXX")
                contenuto = Replace(contenuto, "$filialeappartenenza$", "XXXXXXXXXXXXXXXXXXX")
                contenuto = Replace(contenuto, "$acronimo$", "XXXXXXXXX")
                contenuto = Replace(contenuto, "$protocollo$", "XXXXXXXXX")
                contenuto = Replace(contenuto, "$cds$", "GL0000/XXX/XXXX")
                contenuto = Replace(contenuto, "$tempisticaincomplete$", "XX")
                contenuto = Replace(contenuto, "$tempisticanonrispondenti$", "XX")
                contenuto = Replace(contenuto, "$documenti$", "Documento 1\par Documento 2 \par Documento 3")
                contenuto = Replace(contenuto, "$schedaisee$", "Dati del nominativo + dati isee")

                'MAX 30/09/2015 frontespizio
                contenuto = Replace(contenuto, "$nomeprocesso$", "XXXXXXXXXXXXXXXXXXXXXXX")
                contenuto = Replace(contenuto, "$barcode$", "")
                contenuto = Replace(contenuto, "$documentipresentati$", "Documento 1\par Documento 2 \par Documento 3")



                File.Delete(fileName)

                BaseFile = "MODELLO_" & Format(Now, "yyyyMMddHHmmss")
                file1 = BaseFile & ".RTF"
                fileName = Server.MapPath("..\FileTemp\") & file1
                Dim basefilePDF As String = BaseFile & ".pdf"
                fileNamePDF = Server.MapPath("..\FileTemp\") & basefilePDF

                Dim sr As StreamWriter = New StreamWriter(fileName, False, System.Text.Encoding.Default)
                sr.WriteLine(contenuto)
                sr.Close()

                Dim rp As New Rpn
                Dim i As Boolean
                Dim K As Integer = 0

                'Dim result As Integer = Rpn.RpsSetLicenseInfo("G927S-F6R7A-7VH31", "srab35887-1", "S&S SISTEMI E SOLUZIONI S.R.L.")
                Dim result As Int64 = Rpn.RpsSetLicenseInfo("8RWQS-6Y9UC-HA2L1-91017", "srab35887-1", "S&S SISTEMI E SOLUZIONI S.R.L.")
                rp.InWebServer = True
                rp.EmbedFonts = True
                rp.ExactTextPlacement = True
                i = rp.RpsConvertFile(fileName, fileNamePDF)
                For K = 0 To 2000

                Next
                File.Delete(fileName)
                Response.Write("<script>window.location.href='../FileTemp/" & basefilePDF & "';</script>")

            End If

            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try
    End Sub
End Class
