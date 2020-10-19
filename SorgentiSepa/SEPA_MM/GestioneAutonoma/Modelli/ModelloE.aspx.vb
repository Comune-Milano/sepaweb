Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Partial Class GestioneAutonoma_Modelli_ModelloE
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim NomeFile As String
        Dim idEdificio As String = ""
        Dim idComplesso As String = ""
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            NomeFile = "Id_" & Request.QueryString("IdGestAutonoma") & "_" & Format(Now, "yyyyMMddHHmmss")


            '***************************SELEZIONE DEL FILE BLOB SE ESISTE SUL DATABASE**********************************
            par.cmd.CommandText = "SELECT TESTO FROM SISCOM_MI.AUTOGESTIONI_MODELLI WHERE ID_AUTOGESTIONE = " & Request.QueryString("IdGestAutonoma") & " AND ID_PROVVEDIMENTO = " & Request.QueryString("IdProv") & " AND TIPO_MODELLO = '" & Request.QueryString("TIPO") & "'"
            Dim bw As BinaryWriter
            Dim lettoreblob As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If lettoreblob.Read Then
                Dim fileName As String = Server.MapPath("..\Stampe\" & NomeFile & ".htm")
                Dim fs = New FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write)
                bw = New BinaryWriter(fs)
                bw.Write(lettoreblob("TESTO"))
                bw.Flush()
                bw.Close()
                Dim srl As StreamReader = New StreamReader(Server.MapPath("..\Stampe\" & NomeFile & ".htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                Dim contenutol As String = srl.ReadToEnd()
                srl.Close()

                'scrivo LA NUOVA PROPOSTA DI AUTOGESTIONE
                Dim srlet As StreamWriter = New StreamWriter(Server.MapPath("..\Stampe\" & NomeFile & ".htm"), False, System.Text.Encoding.Default)
                srlet.WriteLine(contenutol)
                srlet.Close()
                Response.Redirect("..\EDITOR_HTML\Editor.aspx?F=" & par.Cripta(NomeFile & ".htm") & "&ID_AUTO=" & Request.QueryString("IdGestAutonoma") & "&IDPROV=" & Request.QueryString("IdProv") & "&TIPO=E")
                Exit Sub
            End If

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\ModE.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()
            par.cmd.CommandText = "SELECT AUTOGESTIONI_PROV.*, AUTOGESTIONI.ID_EDIFICIO,AUTOGESTIONI.ID_COMPLESSO FROM SISCOM_MI.AUTOGESTIONI_PROV,SISCOM_MI.AUTOGESTIONI WHERE AUTOGESTIONI.ID=AUTOGESTIONI_PROV.ID_AUTOGESTIONE AND AUTOGESTIONI_PROV.ID = '" & Request.QueryString("IdProv") & "'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            If myReader.Read Then
                If par.IfNull(myReader("ID_COMPLESSO"), 0) <> 0 Then
                    idEdificio = ""
                    idComplesso = myReader("ID_COMPLESSO")
                Else
                    idComplesso = ""
                    idEdificio = myReader("ID_EDIFICIO")
                End If
                contenuto = Replace(contenuto, "$num_alloggi$", par.IfNull(myReader("NUM_ALLOGGI"), ""))
            End If
            myReader.Close()

            'par.cmd.CommandText = "SELECT AUTOGESTIONI_TAB_SERVIZI.DESCRIZIONE FROM SISCOM_MI.AUTOGESTIONI_SERVIZI, SISCOM_MI.AUTOGESTIONI_TAB_SERVIZI WHERE AUTOGESTIONI_SERVIZI.ID_SERVIZIO = AUTOGESTIONI_TAB_SERVIZI.ID AND ID_MOD_B =" & Request.QueryString("IdProv")
            'myReader = par.cmd.ExecuteReader()
            'Dim GRUPPI As String = ""
            'Do While myReader.Read
            '    GRUPPI = GRUPPI & (par.IfNull(myReader("DESCRIZIONE"), "")) & ";"
            'Loop
            'contenuto = Replace(contenuto, "$gruppo_autogestioni$", GRUPPI)
            'myReader.Close()

            If Not String.IsNullOrEmpty(idEdificio) Then
                par.cmd.CommandText = "SELECT EDIFICI.COD_EDIFICIO AS COD_IMMOBILE, INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO, INDIRIZZI.LOCALITA,INDIRIZZI.CAP,'' AS NUM_SCALE,''AS NUM_PIANI_FUORI FROM SISCOM_MI.INDIRIZZI, SISCOM_MI.EDIFICI WHERE EDIFICI.ID_INDIRIZZO_PRINCIPALE = INDIRIZZI.ID AND EDIFICI.ID = " & idEdificio
            Else
                par.cmd.CommandText = "SELECT COMPLESSI_IMMOBILIARI.COD_COMPLESSO AS COD_IMMOBILE, INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO, INDIRIZZI.LOCALITA,INDIRIZZI.CAP,'' AS NUM_SCALE,''AS NUM_PIANI_FUORI FROM SISCOM_MI.INDIRIZZI, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO = INDIRIZZI.ID AND COMPLESSI_IMMOBILIARI.ID = " & idComplesso
            End If

            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                contenuto = Replace(contenuto, "$indirizzo$", (par.IfNull(myReader("DESCRIZIONE"), "")))
                contenuto = Replace(contenuto, "$civico$", (par.IfNull(myReader("CIVICO"), "")))
                contenuto = Replace(contenuto, "$comune$", (par.IfNull(myReader("LOCALITA"), "")))
                'contenuto = Replace(contenuto, "$cap$", (par.IfNull(myReader("CAP"), "")))
                'contenuto = Replace(contenuto, "$cod_immobile$", (par.IfNull(myReader("COD_IMMOBILE"), "")))
            End If
            myReader.Close()
            par.OracleConn.Close()
            contenuto = Replace(contenuto, "$data_stampa$", Format(Now, "dd/MM/yyyy"))
            'scrivo LA NUOVA PROPOSTA DI AUTOGESTIONE
            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\Stampe\") & NomeFile & ".htm", False, System.Text.Encoding.Default)
            sr.WriteLine(contenuto)
            sr.Close()

            Response.Redirect("..\EDITOR_HTML\Editor.aspx?F=" & par.Cripta(NomeFile & ".htm") & "&ID_AUTO=" & Request.QueryString("IdGestAutonoma") & "&IDPROV=" & Request.QueryString("IdProv") & "&TIPO=E")
            par.OracleConn.Close()


        Catch ex As Exception
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:Gestione Autonoma ModB" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub
End Class
