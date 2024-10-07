using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class CameraControl : MonoBehaviour
{
    public Transform playerTransform;  // Referência ao transform do jogador
    public Vector3 offset;              // Offset da câmera em relação ao jogador
    public float smoothSpeed = 0.125f;  // Velocidade de suavização do movimento da câmera
 
    // Update é chamado uma vez por frame
    void LateUpdate()
    {
        // Calcula a posição desejada da câmera, mantendo o eixo Z fixo
        Vector3 desiredPosition = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z) + offset;
       
        // Suaviza a transição da posição atual para a posição desejada
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
       
        // Atualiza a posição da câmera
        transform.position = smoothPosition;
    }
}
