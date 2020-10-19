
Partial Class Contratti_Tab_Comunicazioni
    Inherits UserControlSetIdMode
    'Public IDUNITAIMMOBILIARE As Long
    Public indiceconnessione As String
    'Public indicecontratto As Long

    Dim PAR As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

    End Sub

    Public Sub Disabilita_tutto()
        txtScalaCor.ReadOnly = True
        txtPianoCor.ReadOnly = True
        txtInternoCor.ReadOnly = True
        txtCAPCor.ReadOnly = True
        txtCivicoCor.ReadOnly = True
        txtLuogoCor.ReadOnly = True
        txtPresso.ReadOnly = True
        txtSiglaCor.ReadOnly = True
        txtViaCor.ReadOnly = True
        cmbTipoViaCor.Enabled = False
        cmbCommissariato.Enabled = False

        'max 23/01/2018
        txtMailCor.Enabled = False
        txtPECCor.Enabled = False
        chkIrreperibile.Enabled = False
    End Sub


    Public Property indicecontratto() As Long
        Get
            If Not (ViewState("par_indicecontratto") Is Nothing) Then
                Return CLng(ViewState("par_indicecontratto"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_indicecontratto") = value
        End Set

    End Property

    Public Property IDUNITAIMMOBILIARE() As Long
        Get
            If Not (ViewState("par_IDUNITAIMMOBILIARE") Is Nothing) Then
                Return CLng(ViewState("par_IDUNITAIMMOBILIARE"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_IDUNITAIMMOBILIARE") = value
        End Set

    End Property


    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"

            If CType(Me.Page.FindControl("confermacambio"), HiddenField).Value = "1" Then


                PAR.OracleConn = CType(HttpContext.Current.Session.Item(indiceconnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & indiceconnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                Dim codice As String = ""
                Dim idl As Long = 0

                PAR.cmd.CommandText = "select cod from comuni_nazioni where upper(nome)='" & PAR.PulisciStrSql(txtLuogoCor.Text) & "'"
                Dim myReaderJJA As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReaderJJA.Read Then
                    codice = PAR.IfNull(myReaderJJA(0), "")
                End If
                myReaderJJA.Close()
                If codice <> "" Then
                    PAR.cmd.CommandText = "Insert into SISCOM_MI.INDIRIZZI  (ID, DESCRIZIONE, CIVICO, CAP, LOCALITA, COD_COMUNE) Values (siscom_mi.seq_indirizzi.nextval, '" & UCase(PAR.PulisciStrSql(cmbTipoViaCor.SelectedItem.Text & " " & txtViaCor.Text)) & "', '" & PAR.PulisciStrSql(txtCivicoCor.Text) & "', '" & PAR.PulisciStrSql(txtCAPCor.Text) & "', '" & PAR.PulisciStrSql(txtLuogoCor.Text) & "', '" & codice & "')"
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "select siscom_mi.seq_indirizzi.currval from dual"
                    myReaderJJA = PAR.cmd.ExecuteReader()
                    If myReaderJJA.Read Then
                        idl = myReaderJJA(0)
                    End If
                    myReaderJJA.Close()

                    PAR.cmd.CommandText = "update siscom_mi.unita_immobiliari set id_indirizzo=" & idl & " where id=" & IDUNITAIMMOBILIARE
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "update siscom_mi.unita_contrattuale set indirizzo='" & UCase(PAR.PulisciStrSql(cmbTipoViaCor.SelectedItem.Text & " " & txtViaCor.Text)) & "'," _
                        & " civico='" & PAR.PulisciStrSql(txtCivicoCor.Text) & "',cap='" & PAR.PulisciStrSql(txtCAPCor.Text) & "',localita='" & PAR.PulisciStrSql(txtLuogoCor.Text) & "'," _
                        & " cod_comune='" & codice & "',scala='" & PAR.PulisciStrSql(txtScalaCor.Text) & "',interno='" & PAR.PulisciStrSql(txtInternoCor.Text) & "',cod_tipo_livello_piano='" & PAR.PulisciStrSql(txtPianoCor.Text) _
                        & "' where id_unita=" & IDUNITAIMMOBILIARE & " and id_contratto=" & indicecontratto
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                    & "VALUES (" & indicecontratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                    & "'F29','')"
                    PAR.cmd.ExecuteNonQuery()

                    CType(Me.Page.FindControl("txtmodificato"), HiddenField).Value = "1"

                    DirectCast(Me.Page.FindControl("Generale1").FindControl("lblIndirizzo"), Label).Text = PAR.PulisciStrSql(txtLuogoCor.Text) & "(" & PAR.PulisciStrSql(txtSiglaCor.Text) & ") CAP " & (txtCAPCor.Text) & " " & UCase(cmbTipoViaCor.SelectedItem.Text & " " & txtViaCor.Text) & ", " & txtCivicoCor.Text

                    Response.Write("<script>alert('Operazione effettuata! Premere il pulsante salva.');</script>")


                Else
                    Response.Write("<script>alert('Luogo non trovato. Non è possibile procedere!');</script>")
                End If
            End If


        Catch ex As Exception

        End Try
    End Sub
End Class
