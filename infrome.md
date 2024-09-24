# Informe de Optimización de Calendarios

## Proyecto CalendarBuilder

![Calendar](images/Calendar.png)

## Autores:

- **Massiel Paz Otaño** (C312) [@NinaSayers](https://github.com/NinaSayers)
- **Marlon Díaz Pérez** (C312) [@MarlonDPerez](https://github.com/MarlonDPerez)
- **Albaro Suárez Valdes** (C312) [@Albaros02](https://github.com/Albaros02)

## Índice

- [Informe de Optimización de Calendarios](#informe-de-optimización-de-calendarios)
  - [Proyecto CalendarBuilder](#proyecto-calendarbuilder)
  - [Autores:](#autores)
  - [Índice](#índice)
  - [Introducción](#introducción)
  - [Algoritmos de Optimización Aplicados](#algoritmos-de-optimización-aplicados)
  - [Formulación del Problema y Restricciones](#formulación-del-problema-y-restricciones)
    - [Datos de Entrada](#datos-de-entrada)
    - [Pasos del Algoritmo Genético](#pasos-del-algoritmo-genético)
  - [Formulación Matemática](#formulación-matemática)
    - [Conjuntos y Parámetros:](#conjuntos-y-parámetros)
    - [Variables de Decisión:](#variables-de-decisión)
    - [Función Objetivo:](#función-objetivo)
    - [Restricciones:](#restricciones)
  - [Librerías y Herramientas Usadas](#librerías-y-herramientas-usadas)
    - [Principales Características](#principales-características)
  - [Modo de Uso](#modo-de-uso)
    - [Manual de Usuario](#manual-de-usuario)
  - [Reporte Técnico](#reporte-técnico)
  - [Conclusiones](#conclusiones)
  - [Referencias](#referencias)

## Introducción

Este informe documenta el proceso de optimización de calendarios para eventos deportivos que involucran múltiples días, sesiones y restricciones específicas. El proyecto *CalendarBuilder* tiene como objetivo asignar deportes a lo largo de varios días, garantizando que se respeten todas las restricciones impuestas, como disponibilidad de recursos, preferencias y capacidades.

El problema incluye dos tipos de sesiones por día (mañana y tarde) y exige que las restricciones de coincidencia entre deportes se cumplan para maximizar la utilidad del calendario.

## Algoritmos de Optimización Aplicados

Para resolver este problema, se utilizó un enfoque basado en algoritmos genéticos similar a como se presenta en el estudio [2]. Dado el amplio espacio de búsqueda generado por la asignación de deportes a sesiones con múltiples restricciones, los algoritmos genéticos proporcionan una solución eficiente. Esta técnica es particularmente útil para encontrar soluciones óptimas en la planificación de recursos con restricciones complejas, como la asignación de deportes para los Juegos Caribe en un período determinado.

## Formulación del Problema y Restricciones

### Datos de Entrada

Los datos de entrada para el problema de optimización del calendario son los siguientes:

1. **Días del Evento**: Intervalo de tiempo del evento deportivo.
2. **Deportes Disponibles**: Lista de deportes a programar.
3. **Restricciones de Coincidencia**: Definen qué deportes no pueden coincidir en el mismo rango de sesiones predefinido.
4. **Cantidad de Sesiones**: Número de sesiones por día que deben ser ocupadas por cada deporte.

### Pasos del Algoritmo Genético

1. **Población Inicial**:
   - Se generan múltiples soluciones iniciales de forma aleatoria, donde cada solución es un calendario que cumple mínimamente las restricciones.

2. **Evaluación de Fitness**:
   - Cada solución se evalúa mediante una función de fitness que mide el grado de cumplimiento de las restricciones. Algunos de los criterios incluyen:
     - Respetar las restricciones de coincidencia.
     - Asignar los deportes de acuerdo con el número de sesiones requerido por cada uno.

3. **Selección**:
   - Se eligen las mejores soluciones basadas en su fitness para la siguiente generación utilizando el método de selección por torneo.

4. **Cruce (Crossover)**:
   - Las soluciones seleccionadas se combinan para generar nuevas soluciones. El cruce mezcla partes de dos calendarios, creando descendientes que heredan características de ambos.

5. **Mutación**:
   - Se introducen modificaciones aleatorias en las soluciones para explorar nuevas combinaciones y evitar quedar atrapados en soluciones subóptimas. Esto puede incluir reasignar un deporte a una sesión diferente.

6. **Evaluación y Evolución**:
   - Las nuevas soluciones generadas se reevalúan y el proceso se repite durante varias generaciones hasta encontrar una solución que cumpla con la mayoría de las restricciones, o hasta alcanzar un límite de tiempo predefinido.

7. **Solución Final**:
   - Tras varias iteraciones, el algoritmo devuelve la mejor solución: un calendario que respeta tantas restricciones como sea posible dentro del marco de tiempo establecido.

## Formulación Matemática

La formulación matemática para este problema de optimización de asignación de deportes al calendario puede representarse como un problema de programación entera o programación lineal mixta (MILP). Los objetivos y restricciones clave se centran en la asignación de deportes a sesiones de manera que se respeten tanto las condiciones de aparición de cada deporte como las restricciones de coincidencia de deportes en sesiones cercanas.

### Conjuntos y Parámetros:

- \( $\mathcal{D} = \{1, 2, \dots, m\}$\): Conjunto de días del evento, donde \( m \) es el número total de días.
- \( $\mathcal{S} = \{1, 2\}$ \): Conjunto de sesiones por día, que incluye la sesión de la mañana (1) y la tarde (2).
- \( $\mathcal{K} = \{1, 2, \dots, n\}$ \): Conjunto de deportes que necesitan ser asignados a las sesiones, donde \( n \) es el número total de deportes.
- \( k_i \): Número total de veces que el deporte \( i \in \mathcal{K} \) debe aparecer en las \( 2m \) sesiones.
- \( $d_{ij}$ \): Mínima distancia permitida entre el deporte \( i \) y el deporte \( j \) en días. Especifica que el deporte \( i \) no puede estar asignado a sesiones en días \( t \) y \( q \) si \( $|t - q| < d_{ij}$ \).

### Variables de Decisión:

- \( $x_{i,t,s} \in \{0, 1\}$ \): Variable binaria que indica si el deporte \( i \) está asignado al día \( t \) y a la sesión \( s \). 
  - \( $x_{i,t,s} = 1$\): Si el deporte \( i \) está programado en el día \( t \) en la sesión \( s \), de lo contrario es 0.

### Función Objetivo:

El objetivo principal es minimizar conflictos y respetar las restricciones de distancia entre deportes, buscando una distribución óptima.

$
\min \sum_{i,j \in \mathcal{K}} \sum_{t,q \in \mathcal{D}} \sum_{s,s' \in \mathcal{S}} c_{ijtqss'} \cdot x_{i,t,s} \cdot x_{j,q,s'}
$

Donde \( $c_{ijtqss'}$ \) es un parámetro que mide el costo o penalización si los deportes \( i \) y \( j \) violan la restricción de distancia en días o se colocan en las mismas o cercanas sesiones.

### Restricciones:

1. **Asignación total**:
   Cada deporte debe aparecer exactamente \( $k_i$ \) veces en las \( 2m \) sesiones de los \( m \) días.

$
\sum_{t \in \mathcal{D}} \sum_{s \in \mathcal{S}} x_{i,t,s} = k_i \quad \forall i \in \mathcal{K}
$

2. **Restricción de sesiones**:
   En cada sesión (mañana o tarde), solo puede asignarse un deporte.

$
\sum_{i \in \mathcal{K}} x_{i,t,s} \leq 1 \quad \forall t \in \mathcal{D}, s \in \mathcal{S}
$

3. **Restricción de distancia**:
   Para cada par de deportes \( i, j \) que tienen una restricción de distancia mínima \( $d_{ij}$ \), se debe garantizar que no aparezcan en días \( t \) y \( q \) donde \( $|t - q| < d_{ij}$ \).

$
x_{i,t,s} + x_{j,q,s'} \leq 1 \quad \forall i, j \in \mathcal{K}, \forall t, q \in \mathcal{D}, |t - q| < d_{ij}, \forall s, s' \in \mathcal{S}
$

Esto asegura que los deportes \( i \) y \( j \) no estén asignados en días demasiado cercanos según su restricción.


## Librerías y Herramientas Usadas

Se utilizaron las siguientes herramientas para implementar y resolver el problema:

- **GeneticSharp**: Biblioteca para la implementación de algoritmos genéticos en C# [1].
- **.NET Core**: Para la creación del backend de la aplicación.
- **Flutter**: Para el desarrollo del frontend.

### Principales Características

- Capacidad de manejar un alto número de restricciones, aumentando la complejidad del problema de manera escalable.
- Eficiencia óptima en problemas de mediana escala.
- Flexibilidad para agregar nuevas restricciones o modificar el modelo del calendario.

## Modo de Uso

### Manual de Usuario

1. **Instalación**: Asegúrese de tener instaladas las librerías necesarias de .NET (`GeneticSharp`, `EntityFrameworkCore`, etc.).
2. **Ejecución de la Aplicación**: Levante los contenedores de Docker con el siguiente comando desde la carpeta de `src`:
   ```bash
   docker-compose up
   ```
3. **Ejecución del Algoritmo**: Desde el frontend, cree un nuevo calendario, agregue los deportes y defina las restricciones. Posteriormente, ejecute el algoritmo para generar el calendario.
4. **Interpretación de Resultados**: El resultado es un calendario de actividades que respeta las restricciones impuestas. Tenga en cuenta que la ejecución puede tardar algunos minutos (hasta un máximo de 5 minutos).

## Reporte Técnico

El algoritmo genético utilizado permite resolver el problema en tiempos computacionales razonables, considerando la cantidad de deportes y días en el evento. Sin embargo, a medida que aumentan las restricciones, la complejidad también incrementa, lo que puede requerir ajustes en los tiempos de ejecución o en la estructura del algoritmo para garantizar soluciones óptimas.

## Conclusiones

La optimización de calendarios es un problema intrínsecamente complejo debido a la necesidad de satisfacer múltiples restricciones. Los algoritmos genéticos se presentaron como una herramienta robusta para encontrar soluciones que respetan estas condiciones. Sin embargo, en casos de mayor escala, podría ser necesario recurrir a otras técnicas heurísticas para obtener soluciones aproximadas en tiempos más cortos.

## Referencias

[1] Giacomelli, "GeneticSharp: A C# Genetic Algorithm Library", Repositorio en GitHub, disponible en: https://github.com/giacomelli/GeneticSharp  
[2] J. Pacheco, A. Aragón, "Diseño de Calendarios de Exámenes en la Universidad: Algoritmos Genéticos", Universidad de Burgos, 2000, disponible en: https://www.asepelt.org/ficheros/File/Anales/2000%20-%20Oviedo/Trabajos/PDF/71.pdf  
