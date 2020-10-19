
Partial Class ANAUT_ListaScarico
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Error
        Dim PageException As String = Server.GetLastError().ToString()
        Dim strBuild As New StringBuilder()
        strBuild.Append("Exception!")
        strBuild.Append(PageException)
        Response.Write(strBuild.ToString())
        Context.ClearError()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim i As Integer
        Dim sPagina As String
        Dim operatore As Long
        Dim operatori As String
        Dim POS As Integer
        Dim POS1 As Integer
        Dim OPERATORE_TR As String

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Expires = 0
        'Response.Write(HttpContext.Current.Session.Item("SCARICO"))
        'HttpContext.Current.Session.Remove("SCARICO")
        'RiempiOperatori()

        If Not IsPostBack Then
            Try
                lN_Distinta = -1
                IOperatore = Request.QueryString("OP")
                Label1.Text = "Estrazione del " & Format(Now, "dd/MM/yyyy") & " Elenco delle Dichiarazioni in carico dal " & par.FormattaData(Request.QueryString("PDA")) & " al " & par.FormattaData(Request.QueryString("PA"))
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Response.Write("IMPOSSIBILE VISUALIZZARE")
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                par.cmd.CommandText = "select utenza_dichiarazioni.ID,utenza_dichiarazioni.PG,TO_CHAR(TO_DATE(utenza_dichiarazioni.DATA_PG,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_PG1"",utenza_dichiarazioni.isee,utenza_COMP_NUCLEO.COGNOME,utenza_COMP_NUCLEO.NOME from utenza_dichiarazioni,utenza_COMP_NUCLEO where utenza_comp_nucleo.progr=0 and utenza_COMP_NUCLEO.ID_DICHIARAZIONE=utenza_dichiarazioni.ID AND utenza_dichiarazioni.ID_CAF=" & Session.Item("ID_CAF") & " AND utenza_dichiarazioni.N_DISTINTA=0 AND utenza_dichiarazioni.id_stato=1 ORDER BY utenza_dichiarazioni.ID ASC"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()


                i = 0
                While myReader.Read
                    par.cmd.CommandText = "SELECT OPERATORI.ID,OPERATORI.OPERATORE AS ""DESCRIZIONE"" FROM OPERATORI,UTENZA_EVENTI_DICHIARAZIONI " _
                    & "WHERE UTENZA_EVENTI_DICHIARAZIONI.TIPO_OPERATORE='I' AND " _
                    & " " _
                    & "UTENZA_EVENTI_DICHIARAZIONI.ID_PRATICA=" & myReader("ID") _
                    & " AND OPERATORI.ID=UTENZA_EVENTI_DICHIARAZIONI.ID_OPERATORE  ORDER BY UTENZA_EVENTI_DICHIARAZIONI.DATA_ORA DESC"
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                    myReader1 = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        operatore = par.IfNull(myReader1("ID"), "")
                        POS = 1
                        While POS <= Len(IOperatore)
                            POS1 = InStr(POS, IOperatore, ",", CompareMethod.Text)
                            If POS1 = 0 Then POS1 = Len(IOperatore) + 1
                            OPERATORE_TR = Mid(IOperatore, POS, POS1 - POS)
                            POS = POS1 + 1
                            If OPERATORE_TR = operatore Then
                                Dim lsiFrutto As New ListItem(myReader("PG") & " " & myReader("DATA_PG1") & " " & FormattaNominativo(myReader("COGNOME") & " " & myReader("NOME")) & " " & par.Tronca(myReader("isee")), myReader("ID"))
                                CheckOperatori.Items.Add(lsiFrutto)
                                lsiFrutto = Nothing
                                Exit While
                            End If
                        End While
                    End If
                    myReader1.Close()
                    i = i + 1
                    'If i = 40 Then Exit While
                End While

                myReader.Close()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                If i = 0 Then
                    Response.Write("<script>alert('Non ci sono domande in carico!');</script>")
                    Exit Sub
                End If
            Catch EX As Exception
                Label1.Text = "ERRORE: " + EX.Message
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End Try
        End If
    End Sub

    Private Function FormattaNominativo(ByVal Nominativo As String) As String
        Dim i As Integer
        Dim NuovoNominativo As String

        If Len(Nominativo) = 50 Then
            FormattaNominativo = Nominativo
            Exit Function
        End If
        If Len(Nominativo) > 50 Then
            FormattaNominativo = Mid(Nominativo, 1, 50)
            Exit Function
        End If
        If Len(Nominativo) < 50 Then
            NuovoNominativo = Nominativo
            For i = Len(Nominativo) To 50
                NuovoNominativo = NuovoNominativo & "&nbsp;"
            Next
            FormattaNominativo = NuovoNominativo
            Exit Function
        End If
    End Function

    Public Property IOperatore() As String
        Get
            If Not (ViewState("par_IOperatore") Is Nothing) Then
                Return CStr(ViewState("par_IOperatore"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_IOperatore") = value
        End Set
    End Property

    Public Property lN_Distinta() As Long
        Get
            If Not (ViewState("par_lN_Distinta") Is Nothing) Then
                Return CLng(ViewState("par_lN_Distinta"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lN_Distinta") = value
        End Set

    End Property

    Protected Sub btnStampa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStampa.Click
        Response.Write("<script>window.print();</script>")

    End Sub

    Protected Sub btnTutti_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTutti.Click
        Dim I As Integer
        For I = 0 To CheckOperatori.Items.Count - 1
            CheckOperatori.Items(I).Selected = True
        Next
    End Sub

    Protected Sub btnScarica_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnScarica.Click
        Dim I As Integer
        Dim J As Integer
        Dim SPagina As String
        Dim N_DISTINTA As Long
        Dim Trovato As Boolean
        Dim Stato As String
        Dim scriptblock As String


        Trovato = False
        For I = 0 To CheckOperatori.Items.Count - 1
            If CheckOperatori.Items(I).Selected = True Then
                Trovato = True
            End If
        Next
        'If Trovato = False Then
        'Response.Write("<script>alert('Nessuna domanda selezionata!');</script>")
        'Exit Sub
        'End If
        J = 0
        For I = 0 To CheckOperatori.Items.Count - 1
            If CheckOperatori.Items(I).Selected = True Then
                J = J + 1
            End If
        Next
        If J > 40 Then
            scriptblock = "<script language='javascript' type='text/javascript'>" _
            & "alert('Selezionare massimo 40 Domanda!');" _
            & "</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
            End If

            Exit Sub
        End If

        If par.OracleConn.State = Data.ConnectionState.Open Then
            Response.Write("IMPOSSIBILE VISUALIZZARE")
            Exit Sub
        Else
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If




        If lN_Distinta = -1 Then
            par.cmd.CommandText = "INSERT INTO UTENZA_DOMANDE_DISTINTE (NUMERO,DATA,ID_OPERATORE) VALUES (SEQ_UTENZA_DOMANDE_DISTINTE.NEXTVAL,'" & Format(Now, "yyyyMMdd") & "'," & Session.Item("ID_OPERATORE") & ")"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "SELECT SEQ_UTENZA_DOMANDE_DISTINTE.CURRVAL FROM DUAL"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                N_DISTINTA = myReader(0)
                lN_Distinta = myReader(0)
            End If
            myReader.Close()
        End If
        'SPagina = "<table border='0' cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf
        'SPagina = SPagina & "<tr>" & vbCrLf
        'SPagina = SPagina & "<td width='100%' colspan='6'>" & vbCrLf
        'SPagina = SPagina & "<p align='center'><b><font face='Arial' size='3'>Distinta N. " & lN_Distinta & " del " & Format(Now, "dd/MM/yyyy") & " creata da: " & Session.Item("OPERATORE") & "</font></b><p>&nbsp;</td>" & vbCrLf
        'SPagina = SPagina & "</tr>" & vbCrLf
        'SPagina = SPagina & "<tr>" & vbCrLf
        'SPagina = SPagina & "<td width='100%' colspan='6'><p>&nbsp;</td>" & vbCrLf
        'SPagina = SPagina & "</tr>" & vbCrLf

        SPagina = "<table border='0' cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf
        SPagina = SPagina & "<tr>" & vbCrLf
        SPagina = SPagina & "<td width='100%' colspan='6'>" & vbCrLf
        SPagina = SPagina & "<p align='center'><b><font face='Arial' size='3'>ANAGRAFE UTENZA - Distinta N. " & lN_Distinta & " del " & Format(Now, "dd/MM/yyyy") & " creata da: " & Session.Item("OPERATORE") & "</font></b><p>&nbsp;</td>" & vbCrLf
        SPagina = SPagina & "</tr>" & vbCrLf
        SPagina = SPagina & "<tr>" & vbCrLf
        SPagina = SPagina & "<td width='100%' colspan='6'><p>&nbsp;</td>" & vbCrLf
        SPagina = SPagina & "</tr>" & vbCrLf
        SPagina = SPagina & "<tr>" & vbCrLf
        SPagina = SPagina & "<td width='6%'><b><font face='Arial' color='#000080' size='2'> </font></b>" & vbCrLf
        SPagina = SPagina & "<p align='left'><b><font face='Arial' size='2'>PROTOCOLLO</font></b></td>" & vbCrLf
        SPagina = SPagina & "<td width='7%'>" & vbCrLf
        SPagina = SPagina & "<p align='left'><font size='2' face='Arial'><b>DATA</b></font></td>" & vbCrLf
        SPagina = SPagina & "<td width='17%'>" & vbCrLf
        SPagina = SPagina & "<p align='left'><b><font size='2' face='Arial'>UTENTE</font></b></td>" & vbCrLf
        SPagina = SPagina & "<td width='6%'>" & vbCrLf
        SPagina = SPagina & "<p align='left'><b><font size='2' face='Arial'>ISEE</font></b></td>" & vbCrLf
        SPagina = SPagina & "<td width='10%'>" & vbCrLf
        SPagina = SPagina & "<p align='left'><b><font size='2' face='Arial'>NOMINATIVO</font></b></td>" & vbCrLf
        SPagina = SPagina & "<td width='66%'></td>" & vbCrLf
        SPagina = SPagina & "</tr>" & vbCrLf
        J = 0
        For I = 0 To CheckOperatori.Items.Count - 1
            If CheckOperatori.Items(I).Selected = True Then
                par.cmd.CommandText = "UPDATE UTENZA_DICHIARAZIONI SET N_DISTINTA=" & lN_Distinta & " WHERE ID='" & CheckOperatori.Items(I).Value & "'"
                par.cmd.ExecuteNonQuery()
                If InStr(CheckOperatori.Items(I).Text, "0,000", CompareMethod.Text) = 0 Then
                    Stato = "7a"
                Else
                    Stato = "4"
                End If

                par.cmd.CommandText = "INSERT INTO UTENZA_EVENTI_DICHIARAZIONI (ID_PRATICA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                            & "VALUES (" & CheckOperatori.Items(I).Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                      & "'F143','Distinta N. " & lN_Distinta & "','I')"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "select UTENZA_DICHIARAZIONI.ID,UTENZA_DICHIARAZIONI.PG,TO_CHAR(TO_DATE(UTENZA_DICHIARAZIONI.DATA_PG,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_PG1"",UTENZA_DICHIARAZIONI.ISEE,UTENZA_COMP_NUCLEO.COGNOME,UTENZA_COMP_NUCLEO.NOME from UTENZA_DICHIARAZIONI,UTENZA_COMP_NUCLEO where utenza_comp_nucleo.progr=0 and UTENZA_COMP_NUCLEO.ID_DICHIARAZIONE=UTENZA_DICHIARAZIONI.ID and UTENZA_DICHIARAZIONI.ID=" & CheckOperatori.Items(I).Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If myReader.Read Then
                    par.cmd.CommandText = "SELECT OPERATORI.ID,OPERATORI.OPERATORE AS ""DESCRIZIONE"" FROM OPERATORI,UTENZA_EVENTI_DICHIARAZIONI " _
                    & "WHERE UTENZA_EVENTI_DICHIARAZIONI.TIPO_OPERATORE='I' AND " _
                    & " " _
                    & "UTENZA_EVENTI_DICHIARAZIONI.ID_PRATICA=" & myReader("ID") _
                    & " AND OPERATORI.ID=UTENZA_EVENTI_DICHIARAZIONI.ID_OPERATORE  ORDER BY UTENZA_EVENTI_DICHIARAZIONI.DATA_ORA DESC"

                    myReader1 = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        SPagina = SPagina & "<tr>" & vbCrLf
                        SPagina = SPagina & "<td width='21%'><p align='left'><font face='Arial' size='2'>" & myReader("PG") & "</font></td>" & vbCrLf
                        SPagina = SPagina & "<td width='12%'><p align='left'><font face='Arial' size='2'>" & myReader("DATA_PG1") & "</font></td>" & vbCrLf
                        SPagina = SPagina & "<td width='15%'><p align='left'><font face='Arial' size='2'>" & par.IfNull(myReader1("DESCRIZIONE"), "") & "</font></td>" & vbCrLf
                        SPagina = SPagina & "<td width='15%'><p align='left'><font face='Arial' size='2'>" & par.Tronca(myReader("ISEE")) & "</font></td>" & vbCrLf
                        SPagina = SPagina & "<td width='44%'><p align='left'><font face='Arial' size='2'>" & myReader("COGNOME") & " " & myReader("NOME") & "</font></td>" & vbCrLf
                        SPagina = SPagina & "<td width='5%'><p align='left'><font face='Arial' size='2'></font></td>" & vbCrLf
                        SPagina = SPagina & "</tr>" & vbCrLf
                    End If
                End If
                'SPagina = SPagina & "<tr>" & vbCrLf
                'SPagina = SPagina & "<td width='100%'><p align='left'><font face='Courier New' size='2'>" & CheckOperatori.Items(I).Text & "</font></td>" & vbCrLf
                'SPagina = SPagina & "</tr>" & vbCrLf
                myReader.Close()
                myReader1.Close()
                J = J + 1
            Else
                par.cmd.CommandText = "UPDATE UTENZA_DICHIARAZIONI SET N_DISTINTA=0 WHERE ID='" & CheckOperatori.Items(I).Value & "'"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM UTENZA_EVENTI_DICHIARAZIONI WHERE ID_PRATICA=" & CheckOperatori.Items(I).Value & " AND TIPO_OPERATORE='I' AND COD_EVENTO='F143'"
                par.cmd.ExecuteNonQuery()
            End If

        Next
        par.cmd.Dispose()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        'SPagina = SPagina & "</table>" & vbCrLf
        SPagina = SPagina & "</table>" & vbCrLf
        SPagina = SPagina & "<p><b>Totale Domande: " & J & "</b></p>"

        Session.Add(Session.Item("ID_OPERATORE"), SPagina)
        Response.Write("<script>window.open('DistintaScarico.aspx','Distinta','');</script>")
    End Sub

    Protected Sub btnChiudi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChiudi.Click
        Response.Write("<script>window.close();</script>")
    End Sub
End Class
