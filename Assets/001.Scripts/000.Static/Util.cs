using UnityEngine;
using System.Collections;

namespace Util
{

    public static class LogicUtils
    {
        //임의의 오브젝트를 기준으로, 그를 포함해 상위를 차례로 검색해 CObject를 찾는다.
        public static CObject FindEntity(Transform objT)
        {
            Transform nowT = objT;

            CObject cObject = null;
            for (int i = 0; i < 10; ++i)
            {
                cObject = nowT.GetComponent<CObject>();
                if (cObject != null) { break; }
                if (nowT.parent == null) { break; }
                nowT = nowT.parent;
            }
            return cObject;
        }

        //사용법 5.Multiplication(2,3) = 40, 5에 2를 3번 곱한다.
        public static int Multiplication(this int var, int a, int b)
        {
            int result = var;
            for (int i = 0; i < b; ++i)
            {
                result *= a;
            }
            return result;
        }

        public static T MinValue<T>(T v1, T v2) where T : System.IComparable<T>
        {
            int com = v1.CompareTo(v2);
            if (com > 0)
            {
                return v2;
            }
            else
            {
                return v1;
            }
        }
        public static T MaxValue<T>(T v1, T v2) where T : System.IComparable<T>
        {
            int com = v1.CompareTo(v2);
            if (com >= 0)
            {
                return v1;
            }
            else
            {
                return v2;
            }
        }


        //방향에 따라 돌아가는 기능
        public static void RotateTransform(Transform objT, Quaternion targetQuaternion)
        {
            objT.rotation = Quaternion.Lerp(objT.rotation, targetQuaternion, 5.5f * Time.deltaTime);
        }
        //방향에 따라 돌아가는 기능
        public static void RotateTransformLocal(Transform objT, Quaternion targetLocalQuaternion)
        {
            objT.localRotation = Quaternion.Lerp(objT.localRotation, targetLocalQuaternion, 5.5f * Time.deltaTime);
        }


        //상대 좌표를 이용한 함수
        public static Vector3 GetLocalVectorFromPosition(Transform org, Vector3 objPosition)
        {
            //org기준으로 구하고 로컬 백터를 구한다. 그 백터 값을 노말라이즈 해서 리턴.
            Vector3 localVector = org.InverseTransformPoint(objPosition);
            return Vector3.Normalize(localVector);
        }
        public static float GetAngleFromTopViewOfLocalVector(Vector3 localVector)
        {
            return Mathf.Atan2(localVector.x, localVector.z) * Mathf.Rad2Deg;
        }
        public static float GetAngleFromForwardViewOfLocalVector(Vector3 localVector)
        {
            return Mathf.Atan2(localVector.x, localVector.y) * Mathf.Rad2Deg;
        }
        public static float GetAngleFromSideViewOfLocalVector(Vector3 localVector)
        {
            return Mathf.Atan2(localVector.y, localVector.z) * Mathf.Rad2Deg;
        }
        public static Vector3 GetVectorFromTopView(float dist, Transform org, Transform obj)
        {
            Vector3 localVector = GetLocalVectorFromPosition(org, obj.position);
            float angle = Mathf.Atan2(localVector.x, localVector.z) * Mathf.Rad2Deg;

            float x, z;
            x = dist * Mathf.Sin(angle * Mathf.Deg2Rad);
            z = dist * Mathf.Cos(angle * Mathf.Deg2Rad);

            return new Vector3(x, 0, z);
        }
        public static Vector3 GetVectorFromForwardView(float dist, Transform org, Transform obj)
        {
            Vector3 localVector = GetLocalVectorFromPosition(org, obj.position);
            float angle = Mathf.Atan2(localVector.x, localVector.y) * Mathf.Rad2Deg;

            float x, y;
            x = dist * Mathf.Sin(angle * Mathf.Deg2Rad);
            y = dist * Mathf.Cos(angle * Mathf.Deg2Rad);

            return new Vector3(x, y, 0);
        }


        public static Vector3[] MakeRandomVectors(Transform objT, int count)
        {
            Vector3[] vector = new Vector3[count];

            float rx;
            float ry;

            float xRange = 360.0f / (float)count;
            float yRange = 360.0f / (float)count;


            for (int i = 0; i < count; ++i)
            {
                rx = UnityEngine.Random.Range(-xRange, xRange);
                ry = UnityEngine.Random.Range(i * yRange, (i + 1) * yRange);
                vector[i] = Quaternion.Euler(rx, ry, 0.0f) * objT.forward;//vector[0] = Quaternion.AngleAxis(r, Vector3.up) * objT.forward;
            }

            return vector;
        }

    }

    public static class StrUtils
    {
        //str에서 cut 시키고 싶은 문자열을 제거
        public static string Cut(string str, string cut)
        {
            int i = str.IndexOf(cut);
            return str.Remove(i);
        }

    }

    public static class NetUtils
    {

    }
   
    public static class JasonUtils
    {

    }

}

