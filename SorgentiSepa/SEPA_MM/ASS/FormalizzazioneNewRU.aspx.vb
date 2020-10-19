
Partial Class ASS_FormalizzazioneNewRU
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            Me.txtDataProvvedimento.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        End If
    End Sub

    Private Function Controlli() As Boolean
        Controlli = False
        Try
            If String.IsNullOrEmpty(txtpg.Text) Then
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Definire il Numero di Protocollo!');", True)
                Exit Function
            End If
            If String.IsNullOrEmpty(txtDataProvvedimento.Text) Then
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Definire la Data del Protocollo!');", True)
                Exit Function
            End If
           
            Controlli = True
        Catch ex As Exception
            Controlli = False
        End Try
    End Function

    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        If Controlli() Then
            Salva()
        End If
    End Sub

    Private Sub Salva()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.myTrans = par.OracleConn.BeginTransaction()
            Dim MyReader As Oracle.DataAccess.Client.OracleDataReader
            Dim idUnita As String = "null"
            idUnita = Request.QueryString("IDU")
            Dim dataAssegnazione As String = Format(Now, "yyyyMMdd")
            Dim GeneratoContratto As Integer = 0
            Dim Cognome As String = ""
            Dim Nome As String = ""
            Dim CFPIVA As String = ""
            Dim IdDichiarazione As String = "null"
            Dim IdAnagrafica As String = "null"
            If Request.QueryString("DICH") <> -1 Then
                IdDichiarazione = Request.QueryString("DICH")
                par.cmd.CommandText = "SELECT NOME, COGNOME, COD_FISCALE FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE = " & IdDichiarazione & " AND PROGR = 0"
                MyReader = par.cmd.ExecuteReader
                If MyReader.Read Then
                    Cognome = par.IfNull(MyReader("COGNOME"), "")
                    Nome = par.IfNull(MyReader("NOME"), "")
                    CFPIVA = par.IfNull(MyReader("COD_FISCALE"), "")
                End If
                MyReader.Close()
            ElseIf Request.QueryString("ANA") <> -1 Then
                IdAnagrafica = Request.QueryString("ANA")
                par.cmd.CommandText = "SELECT NOME, COGNOME, RAGIONE_SOCIALE, COD_FISCALE, PARTITA_IVA, COD_TIPO FROM siscom_mi.ANAGRAFICA WHERE ID = " & IdAnagrafica
                MyReader = par.cmd.ExecuteReader
                If MyReader.Read Then
                    If par.IfNull(MyReader("COD_TIPO"), "F").ToString.ToUpper = "F" Then
                        Cognome = par.IfNull(MyReader("COGNOME"), "")
                        Nome = par.IfNull(MyReader("NOME"), "")
                        CFPIVA = par.IfNull(MyReader("COD_FISCALE"), "")
                    Else
                        Cognome = par.IfNull(MyReader("RAGIONE_SOCIALE"), "")
                        CFPIVA = par.IfNull(MyReader("PARTITA_IVA"), "")
                    End If
                End If
                MyReader.Close()
            End If

            Dim Provvedimento As String = txtpg.Text.ToUpper
            Dim DataProvvedimento As String = par.AggiustaData(txtDataProvvedimento.Text)
            par.cmd.CommandText = "INSERT INTO siscom_mi.UNITA_ASSEGNATE (ID_DOMANDA,ID_UNITA, DATA_ASSEGNAZIONE, GENERATO_CONTRATTO, ID_DICHIARAZIONE, COGNOME_RS, NOME, CF_PIVA,PROVENIENZA, ID_ANAGRAFICA, " _
                                & "PROVVEDIMENTO, DATA_PROVVEDIMENTO, TIPO_APPLICATO) VALUES " _
                                & "(-1," & idUnita & ", '" & dataAssegnazione & "', " & GeneratoContratto & ", " & IdDichiarazione & ", '" & par.PulisciStrSql(Cognome).ToUpper & "', " _
                                & "'" & par.PulisciStrSql(Nome).ToUpper & "', '" & par.PulisciStrSql(CFPIVA).ToUpper & "','E'," & IdAnagrafica & ", " _
                                & "'" & par.PulisciStrSql(Provvedimento).ToUpper & "', '" & DataProvvedimento & "','L. 27/2007')"
            par.cmd.ExecuteNonQuery()

            Dim codUI As String = ""
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID=" & idUnita
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader1.Read = True Then
                codUI = par.IfNull(myReader1("COD_UNITA_IMMOBILIARE"), "")
            End If
            myReader1.Close()

            par.cmd.CommandText = "UPDATE ALLOGGI SET STATO=8,PRENOTATO='1',ASSEGNATO='1',DATA_PRENOTATO='" & Format(Now, "yyyyMMdd") & "',DATA_RESO='" & Format(Now, "yyyyMMdd") & "' WHERE COD_ALLOGGIO='" & codUI & "'"
            par.cmd.ExecuteNonQuery()

            par.myTrans.Commit()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Dim SriptJSContratto As String = ""
            SriptJSContratto = "var chiediConferma;" _
                & "chiediConferma = window.confirm('Operazione Effettuata!\nSi vuole inserire immediatamente il nuovo contratto?');" _
                & "if (chiediConferma == true) { " _
                & "" _
                & "a = window.open('../Contratti/Contratto.aspx?ID=-1&IdDichiarazione=" & IdDichiarazione & "&IdDomanda=-1&IdUnita=" & idUnita & "&TIPO=1&Lett=FM','ContrattoNew" & Format(Now, "hhss") & "','height=780,width=1160');" _
                & "window.close();}" _
                & "else {" _
                & "window.close();" _
                & "}"
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "conf", SriptJSContratto, True)

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.myTrans.Rollback()
                par.OracleConn.Close()
                par.OracleConn.Dispose()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - Salva - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
End Class
