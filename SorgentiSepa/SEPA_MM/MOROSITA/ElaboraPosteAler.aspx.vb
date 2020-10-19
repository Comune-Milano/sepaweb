' ELABORA IL FILE DI RITORNO DI POSTE in FORMATO EXCEL

Imports System.Collections
Imports System.Data.OleDb
Imports System.IO


Partial Class MOROSITA_ElaboraPosteAler
    Inherits PageSetIdMode
    Dim par As New CM.Global

    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
            Exit Sub
        End If

        Response.Expires = 0

        Try

            If Not IsPostBack Then


                ' CONNESSIONE DB
                lIdConnessione = Format(Now, "yyyyMMddHHmmss")

                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Response.Write("IMPOSSIBILE VISUALIZZARE")
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    HttpContext.Current.Session.Add("CONNESSIONE" & lIdConnessione, par.OracleConn)
                End If

                SOLO_LETTURA.Value = "0"

                PulisciCampi()


                Dim CTRL As Control

                '*** FORM PRINCIPALE
                For Each CTRL In Me.form1.Controls
                    If TypeOf CTRL Is TextBox Then
                        DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is DropDownList Then
                        DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is CheckBoxList Then
                        DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                    End If
                Next

                'If Session.Item("MOD_DEM_SL") = "1" Or Mid(par.IfNull(Session.Item("MOD_DEM_IMP"), "0000000000000"), 7, 1) = 0 Then
                '    CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                '    FrmSolaLettura()
                'End If

            End If

        Catch ex As Exception

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try


    End Sub


    Public Property lIdConnessione() As String
        Get
            If Not (ViewState("par_lIdConnessione") Is Nothing) Then
                Return CStr(ViewState("par_lIdConnessione"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_lIdConnessione") = value
        End Set

    End Property



    Private Sub FrmSolaLettura()
        'Disabilita il form (SOLO LETTURA)
        Try

            Dim CTRL As Control = Nothing
            For Each CTRL In Me.form1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).ReadOnly = True
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                ElseIf TypeOf CTRL Is RadioButtonList Then
                    DirectCast(CTRL, RadioButtonList).Enabled = False
                End If
            Next


        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub


    Protected Sub btnElabora_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnElabora.Click
        Dim nFile As String = ""
        Dim EleborazioneInCorso As Integer = 0
        Dim conn1
        Dim rdr As OleDbDataReader

        Dim Conta1 As Integer = 0

        Try
            If FileUpload1.HasFile = True Then

                'Copio il file excel nella cartella temponaea del sistema, alla fine dell'eleborazione verrà eliminato
                nFile = Server.MapPath("..\FileTemp\") & FileUpload1.FileName

                FileUpload1.SaveAs(nFile)

                Dim m_sConn1 As String = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
                                                 "Data Source=" & nFile & ";" & _
                                                 "Extended Properties=""Excel 8.0;HDR=YES"""


                conn1 = New System.Data.OleDb.OleDbConnection(m_sConn1)
                conn1.Open()

                EleborazioneInCorso = 1

                Dim cmd1 As New System.Data.OleDb.OleDbCommand("Select * From [Foglio1$]", conn1)
                rdr = cmd1.ExecuteReader

                Dim dt As New Data.DataTable
                Dim row As System.Data.DataRow

                dt.Columns.Add("DATA")
                dt.Columns.Add("QUANTITA")
                dt.Columns.Add("ARTICOLO")
                dt.Columns.Add("VUOTO1")
                dt.Columns.Add("VUOTO2")
                dt.Columns.Add("TIPOLOGIA")
                dt.Columns.Add("VETTORE")
                dt.Columns.Add("RIF1")
                dt.Columns.Add("VUOTO3")
                dt.Columns.Add("RIF2")
                dt.Columns.Add("RIF3")
                dt.Columns.Add("DESTINATARIO")
                dt.Columns.Add("LOCALITA")
                dt.Columns.Add("BARCODE")
                dt.Columns.Add("LOTTO")
                dt.Columns.Add("COD_ESITO")
                dt.Columns.Add("DESCRIZIONE")
                dt.Columns.Add("DATA_ESITO")
                dt.Columns.Add("IA")
                Dim CodTipoEsito As String = ""
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader

                'Debug.WriteLine(vbCrLf & "EmployeeData:" & vbCrLf & "=============")
                Do While rdr.Read()
                    'Debug.Print(rdr.Item("ID").ToString)

                    EleborazioneInCorso = 2

                    'verifico se il file è quello di postaler
                    If rdr.FieldCount < 19 Then
                        Response.Write("<script>alert('Attenzione... Il file selezionato non è del tipo Excel PosteAler!');</script>")
                        Exit Do
                    End If

                    If rdr.GetName(0) <> "DATA" And rdr.GetName(11) <> "DESTINATARIO" Then
                        Response.Write("<script>alert('Attenzione... Il file selezionato non è del tipo Excel PosteAler!');</script>")
                        Exit Do
                    End If
                    ' RIPRENDO LA CONNESSIONE
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)

                    par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.TIPOLOGIA_ESITI_POSTALER WHERE ID = " & rdr.Item(15).ToString
                    lettore = par.cmd.ExecuteReader
                    If lettore.Read Then
                        CodTipoEsito = par.IfNull(lettore("DESCRIZIONE"), "")
                    End If
                    lettore.Close()
                    row = dt.NewRow()
                    row.Item("DATA") = Strings.Left(rdr.Item(0).ToString, 10)
                    row.Item("QUANTITA") = rdr.Item(1).ToString
                    row.Item("ARTICOLO") = rdr.Item(2).ToString
                    row.Item("VUOTO1") = rdr.Item(3).ToString
                    row.Item("VUOTO2") = rdr.Item(4).ToString
                    row.Item("TIPOLOGIA") = rdr.Item(5).ToString
                    row.Item("VETTORE") = rdr.Item(6).ToString
                    row.Item("RIF1") = rdr.Item(7).ToString
                    row.Item("VUOTO3") = rdr.Item(8).ToString
                    row.Item("RIF2") = rdr.Item(9).ToString
                    row.Item("RIF3") = rdr.Item(10).ToString
                    row.Item("DESTINATARIO") = rdr.Item(11).ToString
                    row.Item("LOCALITA") = rdr.Item(12).ToString
                    row.Item("BARCODE") = rdr.Item(13).ToString
                    row.Item("LOTTO") = rdr.Item(14).ToString
                    row.Item("COD_ESITO") = rdr.Item(15).ToString
                    row.Item("DESCRIZIONE") = CodTipoEsito
                    row.Item("DATA_ESITO") = Strings.Left(rdr.Item(17).ToString, 10)
                    row.Item("IA") = rdr.Item(18).ToString

                    dt.Rows.Add(row)

                    Me.txtVisualizza.Value = 1
                    Me.txtModificato.Value = 1

                    Conta1 = Conta1 + 1

                Loop
                rdr.Close()
                conn1.Close()

                Me.DataGridExcel.Visible = True
                Me.btn_Salva.Visible = True
                Me.lblNote.Visible = True
                Me.txtNote.Visible = True
                EleborazioneInCorso = 0

                DataGridExcel.DataSource = dt
                DataGridExcel.DataBind()

                dt.Dispose()

                File.Delete(nFile)

                Me.txtNote.Text = "ELEMENTI TROVATI: " & Conta1

            Else
                Me.txtVisualizza.Value = 0
                Response.Write("<script>alert('Selezionare il file Excel PosteAler!');</script>")
                PulisciCampi()
            End If


        Catch ex1 As IndexOutOfRangeException

            Response.Write("<script>alert('Attenzione... Il file selezionato non è del tipo Excel PosteAler!');</script>")

            Select Case EleborazioneInCorso
                Case 1
                    conn1.Close()
                Case 2
                    rdr.Close()
                    conn1.Close()

            End Select

            If Strings.Len(nFile) > 0 Then
                File.Delete(nFile)
            End If

            PulisciCampi()

        Catch ex As Exception

            Response.Write("<script>alert('Attenzione... " & ex.Message & "!');</script>")

            Select Case EleborazioneInCorso
                Case 1
                    conn1.Close()
                Case 2
                    rdr.Close()
                    conn1.Close()

            End Select

            If Strings.Len(nFile) > 0 Then
                File.Delete(nFile)
            End If

            PulisciCampi()


        End Try
    End Sub

    Protected Sub btn_Salva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_Salva.Click
        Dim FlagConnessione As String = False
        Dim oDataGridItem As DataGridItem
        Dim ID As Long
        Dim ID_TIPO As Long
        Dim ID_MOROSITA_LETTERE As Long
        Dim sStr1 As String = ""

        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        Dim Righe, RigheSalvate As Integer

        Try

            If Me.txtVisualizza.Value = 0 Then
                Response.Write("<script>alert('Riprovare ad elaborare il file!');</script>")
                Exit Sub

            End If

            If Me.txtModificato.Value = 0 Then
                Response.Write("<script>alert('Riprovare ad elaborare il file!');</script>")
                Exit Sub
            End If


            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'CREO LA TRANSAZIONE
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

            FlagConnessione = True

            If Strings.Len(Me.txtNote.Text) > 0 Then
                Me.txtNote.Text = Me.txtNote.Text & vbCrLf & "ELEMENTI MODIFICATI: "
            End If

            Righe = 0
            RigheSalvate = 0

            'LOOP per tutte le righe del file (se trovo anomalie del file, NON salvo)
            For Each oDataGridItem In Me.DataGridExcel.Items

                Righe = Righe + 1

                If oDataGridItem.Cells(5).Text = "&nbsp;" Then
                    ID = -1
                Else
                    ID = par.IfEmpty(Strings.Trim(oDataGridItem.Cells(5).Text), -1)
                End If

                If ID > 0 Then
                    'par.cmd.CommandText = "SELECT postaler_esiti.ID FROM siscom_mi.postaler, siscom_mi.postaler_esiti " _
                    ' & "WHERE(postaler.ID = postaler_esiti.id_postaler And postaler.id_lettera = " & ID
                Else
                    Response.Write("<SCRIPT>alert('Formato del file non corretto o dati nel file mancanti!');</SCRIPT>")
                    Exit Sub
                End If

                If oDataGridItem.Cells(6).Text = "&nbsp;" Then
                    ID_TIPO = -1
                Else
                    ID_TIPO = par.IfEmpty(Strings.Trim(oDataGridItem.Cells(6).Text), -1)
                End If

                If ID_TIPO = -1 Then
                    Response.Write("<SCRIPT>alert('Attenzione... Il campo COD.ESITO del file non è valorizzato, impossibile continuare!');</SCRIPT>")

                    par.myTrans.Rollback()
                    par.cmd.CommandText = ""
                    FlagConnessione = False

                    Me.txtModificato.Value = 0
                    Exit Sub

                Else
                    sStr1 = ""
                    par.cmd.CommandText = " select ID,DESCRIZIONE from SISCOM_MI.TIPOLOGIA_ESITI_POSTALER where ID=" & ID_TIPO

                    myReader1 = par.cmd.ExecuteReader()
                    If Not myReader1.Read Then
                        myReader1.Close()

                        Response.Write("<SCRIPT>alert('Attenzione... Il campo COD. ESITO del file non è presente nel DataBase, impossibile continuare!');</SCRIPT>")

                        par.myTrans.Rollback()

                        par.cmd.CommandText = ""
                        FlagConnessione = False

                        Me.txtModificato.Value = 0
                        Exit Sub
                    Else
                        sStr1 = "Ricevuta PostAler: " & par.IfNull(myReader1("DESCRIZIONE"), "")

                    End If
                    myReader1.Close()
                End If
                '********************************************************
                par.cmd.CommandText = "SELECT id, TAB_REF  FROM SISCOM_MI.TIPOLOGIA_LETTERE WHERE ID = (SELECT ID_TIPO_LETTERA FROM SISCOM_MI.POSTALER WHERE ID = " & ID & ")"
                Dim TipoLettera As String = ""
                Dim IdTipoLettera As String = ""
                myReader1 = par.cmd.ExecuteReader
                If myReader1.Read Then
                    TipoLettera = par.IfNull(myReader1("TAB_REF"), 0)
                    IdTipoLettera = par.IfNull(myReader1("id"), 0)
                End If
                myReader1.Close()
                If ID > 0 And IdTipoLettera > 0 Then
                    If IdTipoLettera = 1 Then
                        par.cmd.CommandText = " select ID,COD_CONTRATTO,BOLLETTINO " _
                                            & " from SISCOM_MI." & TipoLettera & " " _
                                            & " where ID in (select distinct(ID_LETTERA) " _
                                            & " from SISCOM_MI.POSTALER where ID=" & ID & ")"
                    ElseIf IdTipoLettera = 2 Then
                        par.cmd.CommandText = " select ID,ID_CONTRATTO,ID_AU,DATA_GENERAZIONE " _
                                            & " from SISCOM_MI." & TipoLettera & " " _
                                            & " where ID in (select distinct(ID_LETTERA) " _
                                            & " from SISCOM_MI.POSTALER where ID=" & ID & ")"


                    End If

                    myReader1 = par.cmd.ExecuteReader()
                    If myReader1.Read Then

                        'If Strings.Len(Me.txtNote.Text) > 0 Then
                        '    Me.txtNote.Text = Me.txtNote.Text & vbCrLf & "BOLLETTINO: " & par.IfNull(myReader1("BOLLETTINO"), "") & " del CONTRATTO: " & par.IfNull(myReader1("COD_CONTRATTO"), "") & " esito: " & oDataGridItem.Cells(3).Text
                        'End If
                        ID_MOROSITA_LETTERE = par.IfNull(myReader1("ID"), -1)


                        par.cmd.Parameters.Clear()
                        par.cmd.CommandText = "insert into SISCOM_MI.POSTALER_ESITI " _
                                                    & " (ID,ID_POSTALER,DATA_ESITO,ID_TIPO_ESITI_POSTALER) " _
                                            & "values (SISCOM_MI.SEQ_POSTALER_ESITI.NEXTVAL,:id_postaler,:data,:id_tipo)"

                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_postaler", RitornaNullSeIntegerMenoUno(ID)))
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", par.AggiustaData(oDataGridItem.Cells(4).Text)))

                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_tipo", RitornaNullSeIntegerMenoUno(ID_TIPO)))

                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""
                        par.cmd.Parameters.Clear()
                        '*****************************************




                        ''****************MYEVENT*****************

                        par.cmd.Parameters.Clear()

                        If IdTipoLettera = 1 Then
                            par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_EVENTI " _
                                                & " (ID,ID_MOROSITA_LETTERE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                                & "values (SISCOM_MI.SEQ_MOROSITA_EVENTI.NEXTVAL," & ID_MOROSITA_LETTERE & "," _
                                                & Session.Item("ID_OPERATORE") & "," & Format(Now, "yyyyMMddHHmmss") & "," _
                                                & "'M" & Format(ID_TIPO, "00") & "','" & sStr1 & "')"

                            '' '' '' ''par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", ID_MOROSITA_LETTERE))
                            '' '' '' ''par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

                            '' '' '' ''par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMddHHmmss")))
                            '' '' '' ''par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_evento", "M" & Format(ID_TIPO, "00")))
                            '' '' '' ''par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("motivo", sStr1))

                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = ""
                            par.cmd.Parameters.Clear()
                        ElseIf IdTipoLettera = 2 Then

                            par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_CONTRATTI" _
                                                & " (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                                & "values (" & par.IfNull(myReader1("id_contratto"), 0) & "," & Session.Item("ID_OPERATORE") & "," _
                                                & "'" & Format(Now, "yyyyMMddHHmmss") & "','F175','')"
                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = ""
                            par.cmd.Parameters.Clear()

                        End If



                        '************************************************

                        RigheSalvate = RigheSalvate + 1

                    Else
                        myReader1.Close()
                    End If

                End If
            Next


            If RigheSalvate = Righe Then
                ' COMMIT
                par.myTrans.Commit()
                par.cmd.CommandText = ""
                FlagConnessione = False


                Me.txtModificato.Value = 0
                Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")
            Else
                par.myTrans.Rollback()

                par.cmd.CommandText = ""
                FlagConnessione = False

                Me.txtModificato.Value = 0
                Response.Write("<SCRIPT>alert('Attenzione: codice IA non presente nel DataBase. Nessuna morosità modificata!');</SCRIPT>")

            End If


        Catch ex As Exception

            If FlagConnessione = True Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)

                If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    par.myTrans.Rollback()
                    HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
                End If

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Me.txtModificato.Value = 0

                HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            End If

            Session.Item("LAVORAZIONE") = "0"
            Me.USCITA.Value = "0"

            Page.Dispose()


            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try


    End Sub

    Protected Sub btn_Chiudi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_Chiudi.Click

        If txtModificato.Value <> "111" Then

            If Not CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection) Is Nothing Then

                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)

                If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
                End If

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

            Session.Item("LAVORAZIONE") = "0"

            Page.Dispose()

            Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")

        Else
            txtModificato.Value = "1"
            Me.USCITA.Value = "0"
        End If

    End Sub


    Private Sub PulisciCampi()

        Dim dt As New Data.DataTable

        dt.Columns.Add("DATA")
        dt.Columns.Add("QUANTITA")
        dt.Columns.Add("ARTICOLO")
        dt.Columns.Add("VUOTO1")
        dt.Columns.Add("VUOTO2")
        dt.Columns.Add("TIPOLOGIA")
        dt.Columns.Add("VETTORE")
        dt.Columns.Add("RIF1")
        dt.Columns.Add("VUOTO3")
        dt.Columns.Add("RIF2")
        dt.Columns.Add("RIF3")
        dt.Columns.Add("DESTINATARIO")
        dt.Columns.Add("LOCALITA")
        dt.Columns.Add("BARCODE")
        dt.Columns.Add("LOTTO")
        dt.Columns.Add("COD_ESITO")
        dt.Columns.Add("DESCRIZIONE")
        dt.Columns.Add("DATA_ESITO")
        dt.Columns.Add("IA")


        DataGridExcel.DataSource = dt
        DataGridExcel.DataBind()

        dt.Dispose()

        Me.txtNote.Text = ""

        Me.txtModificato.Value = 0
        Me.txtVisualizza.Value = 0

    End Sub


    Private Function RitornaNullSeIntegerMenoUno(ByVal valorepass As Integer) As Object
        Dim a As Object = DBNull.Value
        Try

            If valorepass <> -1 Then
                a = valorepass
            End If

        Catch ex As Exception

        End Try

        Return a
    End Function

    Private Function RitornaNullSeMenoUno(ByVal valorepass As String) As String
        Dim a As String = "Null"

        Try
            If valorepass = "-1" Then
                a = "Null"
            ElseIf valorepass <> "-1" Then
                a = "'" & valorepass & "'"
            End If

        Catch ex As Exception
        End Try
        Return a

    End Function

    Public Function strToNumber(ByVal s0 As String) As Object
        Dim s As String = s0.Replace(".", ",")

        Dim d As Double

        If Double.TryParse(s, d) = True Then
            Return d
        Else
            Return DBNull.Value
        End If
    End Function

    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function


End Class
