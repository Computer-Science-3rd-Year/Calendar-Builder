# Informe Científico

## Título del Informe
Generación de Calendarios mediante Algoritmos Genéticos

### Autor(es)
Massiel Paz Otaño  
Marlon Díaz Pérez  
Albaro Suárez Valdes

### Fecha
22 de septiembre de 2024

---

## Resumen

Este informe presenta un proyecto innovador que utiliza algoritmos genéticos para la generación de calendarios que cumplen con un conjunto específico de restricciones asociadas a eventos. La lógica aplicada para generar los calendarios se basa en el diseño de calendarios de exámenes en la universidad, tal como se detalla en las referencias. Se implementó una solución en .NET, estructurando el código en dos componentes principales: el backend y un wrapper para el paquete GeneticSharp. Para la gestión de datos, se utilizó una base de datos PostgreSQL. Los resultados obtenidos indican que el enfoque basado en algoritmos genéticos permite una exploración eficiente del espacio de soluciones, logrando calendarios que satisfacen las restricciones establecidas. Se concluye que esta metodología representa una herramienta efectiva para la optimización en la planificación de eventos.

---

## 1. Introducción

- **Contexto**: La planificación de eventos presenta un problema complejo que implica múltiples restricciones, como disponibilidad de recursos, preferencias de los participantes y horarios. Los algoritmos genéticos se han demostrado efectivos en la optimización de problemas combinatorios, ofreciendo soluciones adaptativas a contextos cambiantes.
- **Objetivos**: Este proyecto tiene como objetivo desarrollar un calendario que satisfaga las restricciones impuestas mediante el uso de algoritmos genéticos, optimizando la asignación de eventos.
- **Hipótesis**: Se plantea que la implementación de algoritmos genéticos permitirá generar calendarios que cumplan con un mayor número de restricciones en comparación con métodos tradicionales de optimización.

---

## 2. Metodología

- **Diseño del estudio**: Se llevó a cabo un estudio experimental para evaluar la eficacia de los algoritmos genéticos en la generación de calendarios.Estos estudios estan basados en lo que se expresa en el documento Diseño de Calendarios de Exámenes en la Universidad: Algoritmos Genéticos (ver enlance en referencias). 
- **Población y muestra**: La población objetivo consiste en un conjunto de eventos con restricciones específicas, seleccionando una muestra representativa para el análisis.
- **Instrumentos**: Se empleó el paquete GeneticSharp como base para la implementación de los algoritmos genéticos, junto con una base de datos PostgreSQL para almacenar los eventos y sus respectivas restricciones.
- **Procedimiento**: Se desarrolló un backend en .NET y se creó un wrapper para facilitar el uso de GeneticSharp. Se configuró la base de datos y se realizaron pruebas para generar soluciones óptimas a través de los algoritmos genéticos.

---

## 3. Resultados

- Se generaron múltiples calendarios, cada uno cumpliendo con diferentes configuraciones de restricciones.
- Los resultados indican que el uso de algoritmos genéticos permitió una exploración eficaz del espacio de soluciones, logrando resultados óptimos en un tiempo razonable, destacando la capacidad del sistema para adaptarse a diferentes conjuntos de restricciones.

---

## 4. Discusión

- **Resultados**: En la mayoría de los casos analizados, la aplicación logró proporcionar soluciones aceptadas y alineadas con las restricciones establecidas. Es importante destacar que el tiempo dedicado a la búsqueda de soluciones fue acotado, considerando que algunas configuraciones de restricciones pueden no permitir soluciones viables.
- **Comparación con estudios previos**: En comparación con investigaciones anteriores en planificación de eventos, este enfoque ofrece una solución más dinámica y adaptable, superando limitaciones de metodologías tradicionales.
- **Limitaciones**: Las principales limitaciones incluyen la dependencia de la calidad de las restricciones definidas y el tamaño del espacio de búsqueda, lo que puede afectar la capacidad del algoritmo para encontrar soluciones.
- **Implicaciones**: Los resultados sugieren que esta metodología es aplicable a otros problemas de optimización en diversas áreas, como la logística y la programación de recursos, lo que abre oportunidades para futuras investigaciones.

---

## 5. Conclusiones

- La implementación de algoritmos genéticos en la generación de calendarios permite cumplir de manera efectiva con un conjunto complejo de restricciones.
- Se recomienda investigar la integración de otros métodos de optimización para mejorar los resultados, así como extender las funcionalidades del proyecto para incluir nuevas restricciones y escenarios.

---

## 6. Referencias

- **Diseño de Calendarios de Exámenes en la Universidad: Algoritmos Genéticos**. Departamento de Economía Aplicada, Universidad de Burgos. [Enlace](https://www.asepelt.org/ficheros/File/Anales/2000%20-%20Oviedo/Trabajos/PDF/71.pdf)
- **Repositorio de github de GeneticSharp**[Enlace](https://github.com/giacomelli/GeneticSharp)
