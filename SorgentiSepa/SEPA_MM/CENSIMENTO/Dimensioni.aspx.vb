
Partial Class CENSIMENTO_Dimensioni
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim EsistDimens As Boolean = False
    Dim ValoreCaricato As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            vId = Request.QueryString("ID")
            Passato = Request.QueryString("Pas")
            apri()
        End If

    End Sub
    Private Sub apri()
        Try

            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPOLOGIA_DIMENSIONI"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'Me.DrLDimens.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                'DdLComplesso.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
                DrLDimens.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("COD"), -1)))
            End While
            myReader1.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try


    End Sub
    Public Property vId() As Long
        Get
            If Not (ViewState("par_lIdDichiarazione") Is Nothing) Then
                Return CLng(ViewState("par_lIdDichiarazione"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdDichiarazione") = value
        End Set

    End Property
    Public Property Passato() As String
        Get
            If Not (ViewState("par_Passato") Is Nothing) Then
                Return CStr(ViewState("par_Passato"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Passato") = value
        End Set

    End Property

    Private Sub salva(ByRef esito As Boolean)
        Try
            Dim myreader2 As Oracle.DataAccess.Client.OracleDataReader
            Dim viddimensioni As String
            Dim idGestore As String
            idGestore = Mid(vId, 1, 1)
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans
            If par.IfEmpty(Me.TxtVal.Text, "Null") <> "Null" Then

                Select Case Passato
                    Case "UC"
                        '*************INSERT**********
                        par.cmd.CommandText = ""
                        '10/12/2013 Aggiungo controllo per evitare duplicazioni di inserimento delle superfici : SUP_NETTA; SUP_CONV; SUP_COMM
                        If Me.DrLDimens.SelectedValue.ToString = "SUP_NETTA" Or Me.DrLDimens.SelectedValue.ToString = "SUP_CONV" Or Me.DrLDimens.SelectedValue.ToString = "SUP_COMM" Then
                            par.cmd.CommandText = "select * from siscom_mi.dimensioni where ID_UNITA_COMUNE = " & vId & " and cod_tipologia = '" & Me.DrLDimens.SelectedValue.ToString & "'"
                            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            If lettore.HasRows Then
                                Response.Write("<script>alert('Questa tipologia di dimensione è già stata definita!Non è possibile reinserirla!')</script>")
                                esito = False
                                Exit Sub
                            End If
                            lettore.Close()
                        End If
                        '*******fine controllo*************
                        par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_DIMENSIONI.NEXTVAL FROM DUAL"
                        myreader2 = par.cmd.ExecuteReader
                        If myreader2.Read Then
                            viddimensioni = idGestore & myreader2(0)
                        End If
                        myreader2.Close()
                        If Me.TxtVal.Text <> "" Then
                            par.cmd.CommandText = ""
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.DIMENSIONI (ID, ID_UNITA_COMUNE,COD_TIPOLOGIA,VALORE) VALUES (" & viddimensioni & ", " & vId & ", '" & Me.DrLDimens.SelectedValue.ToString & "', " & par.VirgoleInPunti(Format(Convert.ToDouble(Replace(Me.TxtVal.Text, ",", "."), System.Globalization.CultureInfo.InvariantCulture), "#.00")) & ")"
                            par.cmd.ExecuteNonQuery()
                            '****************MYEVENT*****************
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_UC(ID_UC,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F55','DIMENSIONE TIPO: " & Me.DrLDimens.SelectedItem.Text & " VALORE= " & par.VirgoleInPunti(Format(Convert.ToDouble(Replace(Me.TxtVal.Text, ",", "."), System.Globalization.CultureInfo.InvariantCulture), "#.00")) & "')"
                            par.cmd.ExecuteNonQuery()
                            '*******************************FINE****************************************
                            '***************************************************************************

                            Session.Item("MODIFICASOTTOFORM") = 1

                        End If

                    Case "UI"
                        par.cmd.CommandText = ""
                        '10/12/2013 Aggiungo controllo per evitare duplicazioni di inserimento delle superfici : SUP_NETTA; SUP_CONV; SUP_COMM
                        If Me.DrLDimens.SelectedValue.ToString = "SUP_NETTA" Or Me.DrLDimens.SelectedValue.ToString = "SUP_CONV" Or Me.DrLDimens.SelectedValue.ToString = "SUP_COMM" Then
                            par.cmd.CommandText = "select * from siscom_mi.dimensioni where id_unita_immobiliare = " & vId & " and cod_tipologia = '" & Me.DrLDimens.SelectedValue.ToString & "'"
                            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            If lettore.HasRows Then
                                Response.Write("<script>alert('Questa tipologia di dimensione è già stata definita!Non è possibile reinserirla!')</script>")
                                esito = False
                                Exit Sub
                            End If
                            lettore.Close()
                        End If
                        '*******fine controllo*************
                        par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_DIMENSIONI.NEXTVAL FROM DUAL"
                        myreader2 = par.cmd.ExecuteReader
                        If myreader2.Read Then
                            viddimensioni = idGestore & myreader2(0)
                        End If
                        myreader2.Close()
                        If Me.TxtVal.Text <> "" Then
                            par.cmd.CommandText = ""
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.DIMENSIONI (ID, ID_UNITA_IMMOBILIARE,COD_TIPOLOGIA,VALORE) VALUES (" & viddimensioni & ", " & vId & ", '" & Me.DrLDimens.SelectedValue.ToString & "', " & par.VirgoleInPunti(Format(Convert.ToDouble(Replace(Me.TxtVal.Text, ",", "."), System.Globalization.CultureInfo.InvariantCulture), "#.00")) & ")"
                            par.cmd.ExecuteNonQuery()
                            '****************MYEVENT*****************
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_UI(ID_UI,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F55','DIMENSIONE TIPO: " & Me.DrLDimens.SelectedItem.Text & " VALORE= " & par.VirgoleInPunti(Format(Convert.ToDouble(Replace(Me.TxtVal.Text, ",", "."), System.Globalization.CultureInfo.InvariantCulture), "#.00")) & "')"
                            par.cmd.ExecuteNonQuery()
                            '*******************************FINE****************************************
                            '***************************************************************************
                            Session.Item("MODIFICASOTTOFORM") = 1
                        End If


                    Case "ED"
                        '*************INSERT**********
                        par.cmd.CommandText = ""
                        '10/12/2013 Aggiungo controllo per evitare duplicazioni di inserimento delle superfici : SUP_NETTA; SUP_CONV; SUP_COMM
                        If Me.DrLDimens.SelectedValue.ToString = "SUP_NETTA" Or Me.DrLDimens.SelectedValue.ToString = "SUP_CONV" Or Me.DrLDimens.SelectedValue.ToString = "SUP_COMM" Then
                            par.cmd.CommandText = "select * from siscom_mi.dimensioni where ID_EDIFICIO = " & vId & " and cod_tipologia = '" & Me.DrLDimens.SelectedValue.ToString & "'"
                            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            If lettore.HasRows Then
                                Response.Write("<script>alert('Questa tipologia di dimensione è già stata definita!Non è possibile reinserirla!')</script>")
                                esito = False
                                Exit Sub
                            End If
                            lettore.Close()
                        End If
                        '*******fine controllo*************
                        par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_DIMENSIONI.NEXTVAL FROM DUAL"
                        myreader2 = par.cmd.ExecuteReader
                        If myreader2.Read Then
                            viddimensioni = idGestore & myreader2(0)
                        End If
                        myreader2.Close()
                        If Me.TxtVal.Text <> "" Then
                            par.cmd.CommandText = ""
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.DIMENSIONI (ID, ID_EDIFICIO,COD_TIPOLOGIA,VALORE) VALUES (" & viddimensioni & ", " & vId & ", '" & Me.DrLDimens.SelectedValue.ToString & "', " & par.VirgoleInPunti(Format(Convert.ToDouble(Replace(Me.TxtVal.Text, ",", "."), System.Globalization.CultureInfo.InvariantCulture), "#.00")) & ")"
                            par.cmd.ExecuteNonQuery()
                            '****************MYEVENT*****************
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_EDIFICI(ID_EDIFICIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F55','DIMENSIONE TIPO: " & Me.DrLDimens.SelectedItem.Text & " VALORE= " & par.VirgoleInPunti(Format(Convert.ToDouble(Replace(Me.TxtVal.Text, ",", "."), System.Globalization.CultureInfo.InvariantCulture), "#.00")) & "')"
                            par.cmd.ExecuteNonQuery()
                            '*******************************FINE****************************************
                            '***************************************************************************
                            Session.Item("MODIFICASOTTOFORM") = 1
                        End If
                End Select
                esito = True
            Else
                Response.Write("<SCRIPT>alert('Nessun valore inserito!');</SCRIPT>")

            End If

            'End If
        Catch ex As Exception
            esito = False
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

        End Try
        'Response.Write("<SCRIPT>alert('Salvataggio completato correttamente!');</SCRIPT>")

    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Response.Write("<script>opener.document.getElementById('form1').submit();window.close();</script>")

    End Sub

    Protected Sub BtnADD_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnADD.Click
        Dim risultato As Boolean
        Me.salva(risultato)
        If risultato = True Then
            Response.Write("<script>opener.document.getElementById('form1').submit();window.close();</script>")
        End If
    End Sub
End Class
