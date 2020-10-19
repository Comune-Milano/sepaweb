
Partial Class Contratti_Tab_Comunicazioni
    Inherits UserControlSetIdMode
    Public indiceconnessione As String



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


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub
End Class
