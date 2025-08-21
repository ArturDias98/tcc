
# Fuzzy Level Supervision and Control: OPC-Based Integration

## Abstract

In the field of industrial automation, integration between control systems is essential for efficient monitoring and control of production processes. This project presents the development of a level supervision and control system based on Fuzzy logic, integrated through the OPC (Open Platform Communications) protocol.

Fuzzy logic, an artificial intelligence technique, enables the handling of uncertainties and imprecisions inherent to industrial processes. It is particularly suitable for level plant control, which is common in petrochemical, pharmaceutical, and food and beverage industries. These systems are nonlinear and often multivariable, presenting significant challenges for conventional control techniques.

The main objective is to implement an integration architecture between an API responsible for Fuzzy control and a simulated level plant, using the OPC protocol as the communication medium. This approach demonstrates the feasibility of applying artificial intelligence techniques in real industrial environments, leveraging the standardization and widespread adoption of OPC in modern industry.

## Objectives

- Develop a desktop application that enables integration with any supervisory system using the OPC UA communication protocol.
- Adopt a Fuzzy control model, with parameterization performed directly in the API.
- Evaluate the possibility of using the API locally, without the need for publication.
- Provide a user interface for OPC server configuration and tag management for reading and writing, utilizing the API for Fuzzy control.
- Offer an installer for easy software deployment.

## Project Structure

- **Fuzzy Control API**: Implements the Fuzzy logic controller and exposes endpoints for integration.
- **OPC UA Client**: Communicates with the simulated level plant and supervisory systems.
- **Desktop Application**: User interface for configuration and monitoring.
- **Documentation**: Includes theoretical background, methodology, and results.

## Usage

1. Clone the repository.
2. Install dependencies as described in the respective `requirements.txt` files.
3. Configure the OPC UA server and tags using the desktop application.
4. Start the Fuzzy Control API and connect it to the simulated plant.
5. Monitor and control the process through the user interface.

## Future Work

- Publish the API to enable mobile application integration.
- Extend robustness evaluation with latency disturbances.
- Compare results with other control techniques.

## Acknowledgments

Special thanks to God for strength and perseverance, to my family for unconditional support, and to all professors for their guidance and dedication. I also acknowledge the Federal Institute of Espírito Santo for providing resources, opportunities, and a conducive environment for learning and development.
